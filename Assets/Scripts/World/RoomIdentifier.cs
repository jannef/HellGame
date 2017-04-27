using fi.tamk.hellgame.character;
using fi.tamk.hellgame.dataholders;
using fi.tamk.hellgame.input;
using fi.tamk.hellgame.ui;
using fi.tamk.hellgame.utils;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.Events;

namespace fi.tamk.hellgame.world
{
    [Serializable]
    public struct PoolInstruction
    {
        [SerializeField] public GameObject Prefab;
        [SerializeField] public int HowMany;
    }

    public sealed class RoomIdentifier : MonoBehaviour
    {
        [HideInInspector] public int SceneId;
        public static float RoomCompletionTime;        
        private Transform _playerSpawnPoint;

        [SerializeField] private bool _spawnPlayer = true;
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] public RoomClearingRanks roomClearingRankField;
        [SerializeField] private PoolInstruction[] PoolingInstructions;        
        [SerializeField] private bool _isMenuScene = false;

        public UnityEvent OnPlayerVictory;
        public UnityEvent OnRoomStart;

        public static RoomClearingRanks Ranks;
        public static event Action PlayerDeath;
        public static event Action RoomCompleted;
        public static event Action<ClearingRank> RankGained;
        public static event Action GamePaused;
        public static event Action GameResumed;
        public static event Action EncounterBegin;

        private static List<Action> _onPausedActions = new List<Action>();
        private static List<Action> _onGameResumeActions = new List<Action>();
        private static List<Action> _onEncounterBeginActions = new List<Action>();
        private static bool _isGamePaused = false;

        private string _roomName = "????";
        private BottomHUD _bottomHud;
        private TextMeshProUGUI _clockField;
        private static GUIReferences _guiReferences;
        private static GameClock _clock;

        private void Awake()
        {
            var roomManager = FindObjectOfType<RoomManager>();
            if (roomManager == null)
            {
                SceneLoadLock.SceneChangeInProgress = true;
                DontDestroyOnLoad(gameObject);
                SceneId = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(0);
                return;
            }
            // The function has returned if this is a debug run
            if (OnRoomStart != null) OnRoomStart.Invoke();
            if (_isMenuScene) return;
            _roomName = UserStaticData.IndexToName(SceneManager.GetActiveScene().buildIndex);
            Init();
            SpawnPlayer();

            if (PoolingInstructions != null)
            {
                foreach (var pi in PoolingInstructions)
                {
                    Pool.Instance.AddToPool(pi.Prefab, pi.HowMany);
                }
            }

            if (roomClearingRankField == null)
            {
                Debug.LogWarning(string.Format("Room {0} does not have clearing ranks set, falling back to placeholders!", _roomName));
                roomClearingRankField = Instantiate(Resources.Load("PlaceholderRanks") as RoomClearingRanks);
            }

            if (_bottomHud != null && !RoomManager.RetryFlag) _bottomHud.DisplayMessage(_roomName);
        }

        private void SpawnPlayer()
        {
            GameObject go = null;
            var playerPrefab = FindObjectOfType<PlayerLimitBreak>();
            if (playerPrefab != null)
            {
                go = playerPrefab.gameObject;
                playerPrefab.transform.position = _playerSpawnPoint.position;
            }
            else if (_spawnPlayer)
            {
                go = Instantiate(_playerPrefab, _playerSpawnPoint.position, Quaternion.identity);
            }

            if (go != null)
            {
                var hc = go.GetComponent<HealthComponent>();
                // Listener for player death
                if (hc != null)
                {
                    hc.DeathEffect.AddListener(OnPlayerDeath);
                }

                if (!_spawnPlayer) return;
                var ic = go.GetComponent<InputController>();

                var spawner = GetComponentInChildren<PlayerSpawner>();

                if (spawner != null)
                {
                    spawner.StartSpawning(go, RoomManager.LastSceneIndex);
                }

                if (RoomManager.PlayerPersistentData == null) return;
                if (hc != null && hc.MaxHp != RoomManager.PlayerPersistentData.Health)
                {
                    hc.Health = (hc.MaxHp - RoomManager.PlayerPersistentData.Health);
                }
                if (ic != null && RoomManager.PlayerPersistentData.MyConfig != null)
                {
                    ic.MyConfig = RoomManager.PlayerPersistentData.MyConfig;
                }
            }
        }

        private void Init()
        {
            Ranks = roomClearingRankField;
            GamePaused = null;
            GameResumed = null;
            RankGained = null;
            PlayerDeath = null;
            RoomCompleted = null;
            EncounterBegin = null;
            SceneManager.sceneLoaded += InitializeAtSceneStart;
            RoomCompleted += RoomClearedSave;
            SceneId = SceneManager.GetActiveScene().buildIndex;

            _isGamePaused = false;
            _playerSpawnPoint = GetComponentInChildren<Transform>();
            _guiReferences = FindObjectOfType<GUIReferences>() ?? new UnityException("GUIReferences not found in a scene!").Throw<GUIReferences>();
            _clockField = _guiReferences.ClockText;
            _bottomHud = GetComponent<BottomHUD>();
            _clock = gameObject.GetOrAddComponent<GameClock>();
            _clock.Init(_clockField);
        }

        private void RoomClearedSave()
        {
            RoomCompleted -= RoomClearedSave;
            UserStaticData.RoomClearedSave(new RoomSaveData(SceneId, RoomCompletionTime));
        }

        private void InitializeAtSceneStart(Scene scene, LoadSceneMode mode)
        {
            SceneManager.sceneLoaded -= InitializeAtSceneStart;
            

            foreach (var action in _onPausedActions)
            {
                GamePaused += action;
            }

            foreach (var action in _onGameResumeActions)
            {
                GameResumed += action;
            }

            foreach (var action in _onEncounterBeginActions)
            {
                Debug.Log("Initialize EncounterBegin");
                EncounterBegin += action;
            }

            _onGameResumeActions.Clear();
            _onPausedActions.Clear();
            _onEncounterBeginActions.Clear();
        }

        private void OnPlayerDeath()
        {
            WorldStateMachine.Instance.InterPolatingSlowDown(Mathf.Infinity, 0, 1.5f);
            if (PlayerDeath != null) PlayerDeath.Invoke();
        }

        public static void OnRoomCompleted(RoomClearingRanks ranks = null)
        {
            RoomCompletionTime = _clock.StopClock(); //stops the clock and returns current time.
            if (RoomCompleted != null) RoomCompleted.Invoke();
            var score = ScoreWindow.GetScoreWindowGo(_guiReferences.transform);
            var player = ServiceLocator.Instance.GetNearestPlayer().gameObject.GetComponent<HealthComponent>();

            var ri = FindObjectOfType<RoomIdentifier>();
            score.SetData(_clock, player.MaxHp - player.Health, ranks == null ? ri.roomClearingRankField : ranks);
            if (ri.OnPlayerVictory != null) ri.OnPlayerVictory.Invoke();
        }

        public void RoomCompletedTrigger()
        {
            OnRoomCompleted(roomClearingRankField);
        }

        public static void BeginEncounter()
        {
            if (EncounterBegin != null) EncounterBegin.Invoke();
        }

        public static void PauseGame()
        {
            if (_isGamePaused)
            {
                if (GameResumed != null) GameResumed.Invoke();
            } else
            {
                if (GamePaused != null) GamePaused.Invoke();
            }

            _isGamePaused = !_isGamePaused;
        }

        public static void AddOnPauseListenerAtAwake(Action action)
        {
            if (_onPausedActions == null) _onPausedActions = new List<Action>();
            _onPausedActions.Add(action);
        }

        public static void AddEncounterBeginListenerAtAwake(Action action)
        {
            if (_onEncounterBeginActions == null) _onEncounterBeginActions = new List<Action>();
            _onEncounterBeginActions.Add(action);
        }

        public static void AddOnResumeListenerAtAwake(Action action)
        {
            if (_onGameResumeActions == null) _onGameResumeActions = new List<Action>();
            _onGameResumeActions.Add(action);
        }
    }
}
