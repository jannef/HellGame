using fi.tamk.hellgame.character;
using fi.tamk.hellgame.input;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace fi.tamk.hellgame.world
{
    public sealed class RoomIdentifier : MonoBehaviour
    {
        public int SceneId;
        private Transform _playerSpawnPoint;
        [SerializeField] private GameObject _playerPrefab;

        private void Awake()
        {
            _playerSpawnPoint = GetComponentInChildren<Transform>();

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
                GameObject go;
                if (playerPrefab != null)
                {
                    go = playerPrefab.gameObject;
                    playerPrefab.transform.position = _playerSpawnPoint.position;                    
                }
                else
                {
                    go = Instantiate(_playerPrefab, _playerSpawnPoint.position, Quaternion.identity);
                    playerPrefab = go.GetComponent<PlayerLimitBreak>();
                }

                if (roomManager.PlayerPersistentData != null)
                {                
                    var hc = go.GetComponent<HealthComponent>();
                    var ic = go.GetComponent<InputController>();
                    if (hc != null && hc.MaxHp != hc.Health) hc.TakeDamage(hc.MaxHp - roomManager.PlayerPersistentData.Health);
                    if (ic != null && roomManager.PlayerPersistentData.MyInput != null) ic.MyConfig = roomManager.PlayerPersistentData.MyInput;
                }
            }
        }
    }
}
