using UnityEngine;
using UnityEngine.SceneManagement;

namespace fi.tamk.hellgame.world
{
    public sealed class RoomIdentifier : MonoBehaviour
    {
        public int SceneId;

        private void Awake()
        {
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
            }
        }
    }
}
