using fi.tamk.hellgame.character;
using fi.tamk.hellgame.dataholders;
using fi.tamk.hellgame.input;
using fi.tamk.hellgame.ui;
using fi.tamk.hellgame.utils;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

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
        [SerializeField] private RoomClearingRanks roomClearingRankField;
        [SerializeField] private PoolInstruction[] PoolingInstructions;
        [SerializeField] private string RoomName;

        public static RoomClearingRanks Ranks;
        public static event Action PlayerDeath;
        public static event Action RoomCompleted;
        public static event Action<ClearingRank> RankGained;
        public static event Action GamePaused;
        public static event Action GameResumed;        
        public static bool IsPartOfWingRun = false;

        private static List<Action> _onPausedActions = new List<Action>();
        private static List<Action> _onGameResumeActions = new List<Action>();
        private static bool _isGamePaused = false;

        private BottomHUD _bottomHud;
        private TextMeshProUGUI _clockField;
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
            Init();
            SpawnPlayer();

            if (PoolingInstructions != null)
            {
                foreach (var pi in PoolingInstructions)
                {
                    Pool.Instance.AddToPool(pi.Prefab, pi.HowMany);
                }
            }
            
            _bottomHud.DisplayMessage(RoomName);
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
            SceneManager.sceneLoaded += InitializeAtSceneStart;
            RoomCompleted += RoomClearedSave;
            SceneId = SceneManager.GetActiveScene().buildIndex;

            _isGamePaused = false;
            _playerSpawnPoint = GetComponentInChildren<Transform>();
            _clockField = (FindObjectOfType<GUIReferences>() ?? new UnityException("GUIReferences not found in a scene!").Throw<GUIReferences>()).ClockText;
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

            _onGameResumeActions.Clear();
            _onPausedActions.Clear();
        }

        private void OnPlayerDeath()
        {
            WorldStateMachine.Instance.PauseTime();
            if (PlayerDeath != null) PlayerDeath.Invoke();
        }

        public static void OnRoomCompleted()
        {
            if (RoomCompleted != null) RoomCompleted.Invoke();
            OnRankGained(RoomIdentifier.RoomCompletionTime);
        }

        public static void OnRankGained(float time)
        {
            if (RankGained != null && Ranks != null) RankGained.Invoke(Ranks.GetRankFromTime(time));
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

        public static void AddOnResumeListenerAtAwake(Action action)
        {
            if (_onGameResumeActions == null) _onGameResumeActions = new List<Action>();
            _onGameResumeActions.Add(action);
        }
    }
}
