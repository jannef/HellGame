using fi.tamk.hellgame.character;
using fi.tamk.hellgame.dataholders;
using fi.tamk.hellgame.input;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace fi.tamk.hellgame.world
{
    public sealed class RoomIdentifier : MonoBehaviour
    {
        public int SceneId;
        public static float RoomCompletionTime;
        [SerializeField] private bool _spawnPlayer = true;
        private Transform _playerSpawnPoint;
        [SerializeField] private GameObject _playerPrefab;

        public static event Action PlayerDeath;
        public static event Action RoomCompleted;

        public static event Action GamePaused;
        public static event Action GameResumed;
        private static bool isGamePaused = false;

        private static List<Action> _onPausedActions = new List<Action>();
        private static List<Action> _onGameResumeActions = new List<Action>();

        private void Awake()
        {
            _playerSpawnPoint = GetComponentInChildren<Transform>();
            GameObject go = null;
            GamePaused = null;
            isGamePaused = false;
            GameResumed = null;
            PlayerDeath = null;
            RoomCompleted = null;
            HealthComponent hc = null;
            SceneManager.sceneLoaded += InitializeAtSceneStart;
            RoomCompleted += RoomClearedSave;

            var roomManager = FindObjectOfType<RoomManager>();
            if (roomManager == null)
            {
                SceneLoadLock.SceneChangeInProgress = true;
                DontDestroyOnLoad(gameObject);
                SceneId = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(0);
            }
            else
            {
                gameObject.SetActive(false);
                var playerPrefab = FindObjectOfType<PlayerLimitBreak>();
                
                if (playerPrefab != null)
                {
                    go = playerPrefab.gameObject;
                    playerPrefab.transform.position = _playerSpawnPoint.position;                    
                }
                else if (_spawnPlayer)
                {
                    go = Instantiate(_playerPrefab, _playerSpawnPoint.position, Quaternion.identity);
                    playerPrefab = go.GetComponent<PlayerLimitBreak>();
                }

                if (go != null)
                {
                    hc = go.GetComponent<HealthComponent>();
                    // Listener for player death
                    if (hc != null) {
                        hc.DeathEffect.AddListener(OnPlayerDeath); 
                    }

                }

                if (RoomManager.PlayerPersistentData != null && _spawnPlayer)
                {                
                    
                    var ic = go.GetComponent<InputController>();
                    if (hc != null && hc.MaxHp != hc.Health) hc.TakeDamage(hc.MaxHp - RoomManager.PlayerPersistentData.Health);
                    if (ic != null && RoomManager.PlayerPersistentData.MyConfig != null) ic.MyConfig = RoomManager.PlayerPersistentData.MyConfig;
                }
            }
        }

        private void RoomClearedSave()
        {
            RoomCompleted -= RoomClearedSave;
            UserStaticData.RoomClearedSave(new RoomSaveData(SceneId, RoomCompletionTime));
        }

        private void InitializeAtSceneStart(Scene scene, LoadSceneMode mode)
        {
            SceneManager.sceneLoaded -= InitializeAtSceneStart;

            foreach (Action action in _onPausedActions)
            {
                GamePaused += action;
            }

            foreach (Action action in _onGameResumeActions)
            {
                GameResumed += action;
            }

            _onGameResumeActions.Clear();
            _onPausedActions.Clear();
        }

        private void OnPlayerDeath()
        {
            if (PlayerDeath != null) PlayerDeath.Invoke();
        }

        public static void OnRoomCompleted()
        {
            if (RoomCompleted != null) RoomCompleted.Invoke();
        }

        public static void PauseGame()
        {
            if (isGamePaused)
            {
                if (GameResumed != null) GameResumed.Invoke();
            } else
            {
                if (GamePaused != null) GamePaused.Invoke();
            }

            isGamePaused = !isGamePaused;
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
