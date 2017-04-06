﻿using fi.tamk.hellgame.character;
using fi.tamk.hellgame.dataholders;
using fi.tamk.hellgame.input;
using fi.tamk.hellgame.utils;
using System;
using System.Collections.Generic;
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
        [SerializeField] private bool _spawnPlayer = true;
        private Transform _playerSpawnPoint;
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private RoomClearingRanks roomClearingRankField;
        public static RoomClearingRanks Ranks;

        public static event Action PlayerDeath;
        public static event Action RoomCompleted;
        public static event Action<ClearingRank> RankGained;

        public static event Action GamePaused;
        public static event Action GameResumed;
        private static bool isGamePaused = false;
        public static bool IsPartOfWingRun = false;

        private static List<Action> _onPausedActions = new List<Action>();
        private static List<Action> _onGameResumeActions = new List<Action>();

        [SerializeField] private PoolInstruction[] PoolingInstructions;

        private void Awake()
        {
            Ranks = roomClearingRankField;
            _playerSpawnPoint = GetComponentInChildren<Transform>();
            GameObject go = null;
            GamePaused = null;
            isGamePaused = false;
            GameResumed = null;
            RankGained = null;
            PlayerDeath = null;
            RoomCompleted = null;
            HealthComponent hc = null;
            SceneManager.sceneLoaded += InitializeAtSceneStart;
            RoomCompleted += RoomClearedSave;
            SceneId = SceneManager.GetActiveScene().buildIndex;

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

                if (_spawnPlayer)
                {
                    var ic = go.GetComponent<InputController>();
                    
                    if (RoomManager.PlayerPersistentData != null)
                    {
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

                if (PoolingInstructions != null)
                {
                    foreach (var pi in PoolingInstructions)
                    {
                        Pool.Instance.AddToPool(pi.Prefab, pi.HowMany);
                    }
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
            OnRankGained(RoomIdentifier.RoomCompletionTime);
        }

        public static void OnRankGained(float time)
        {
            if (RankGained != null && Ranks != null) RankGained.Invoke(Ranks.GetRankFromTime(time));
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
