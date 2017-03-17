using fi.tamk.hellgame.character;
using fi.tamk.hellgame.input;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace fi.tamk.hellgame.world
{
    public sealed class RoomIdentifier : MonoBehaviour
    {
        public int SceneId;
        [SerializeField] private bool _spawnPlayer = true;
        private Transform _playerSpawnPoint;
        [SerializeField] private GameObject _playerPrefab;

        public static event Action PlayerDeath;
        public static event Action EncounterStart;
        public static event Action RoomCompleted;

        private void Awake()
        {
            _playerSpawnPoint = GetComponentInChildren<Transform>();
            GameObject go = null;
            PlayerDeath = null;
            RoomCompleted = null;
            EncounterStart = null;
            HealthComponent hc = null;

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

        private void OnPlayerDeath()
        {
            if (PlayerDeath != null) PlayerDeath.Invoke();
        }

        public static void OnRoomCompleted()
        {
            if (RoomCompleted != null) RoomCompleted.Invoke();
        }

        public static void OnEncounterBegin()
        {
            if (EncounterStart != null) EncounterStart.Invoke();
        }
    }
}
