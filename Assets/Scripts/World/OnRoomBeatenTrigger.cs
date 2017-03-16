using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace fi.tamk.hellgame.world
{

    public class OnRoomBeatenTrigger : MonoBehaviour
    {
        public UnityEvent OnRoomBeatenEvent;

        private void Awake()
        {
            SceneManager.sceneLoaded += Initialize;
        }

        private void Initialize(Scene scene, LoadSceneMode mode)
        {
            SceneManager.sceneLoaded -= Initialize;
            RoomIdentifier.RoomCompleted += Activate;
        }

        public void Activate()
        {
            if (OnRoomBeatenEvent != null) OnRoomBeatenEvent.Invoke();
            RoomIdentifier.RoomCompleted -= Activate;
        }
    }
}
