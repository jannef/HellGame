using fi.tamk.hellgame.character;
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

            if (FindObjectOfType<RoomManager>() == null)
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
                    playerPrefab.transform.position = _playerSpawnPoint.position;
                }
                else
                {
                    Instantiate(_playerPrefab, _playerSpawnPoint.position, Quaternion.identity);
                }
            }
        }
    }
}
