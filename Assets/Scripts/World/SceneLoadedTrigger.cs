using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace fi.tamk.hellgame.world
{

    public class SceneLoadedTrigger : MonoBehaviour
    {
        public UnityEvent SceneLoadedEvent;

        private void Awake()
        {
            SceneManager.sceneLoaded += Initialize;
        }

        private void Initialize(Scene scene, LoadSceneMode mode)
        {
            SceneManager.sceneLoaded -= Initialize;
            if (SceneLoadedEvent != null) SceneLoadedEvent.Invoke();
        }
    }
}
