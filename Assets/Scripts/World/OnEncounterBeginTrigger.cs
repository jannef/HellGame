using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace fi.tamk.hellgame.world
{

    public class OnEncounterBeginTrigger : MonoBehaviour
    {
        public UnityEvent OnEncounterBeginEvent;

        private void Awake()
        {
            SceneManager.sceneLoaded += Initialize;
        }

        private void Initialize(Scene scene, LoadSceneMode mode)
        {
            SceneManager.sceneLoaded -= Initialize;
            RoomIdentifier.EncounterBegin += Activate;
        }

        public void Activate()
        {
            if (OnEncounterBeginEvent != null) OnEncounterBeginEvent.Invoke();
            RoomIdentifier.EncounterBegin -= Activate;
        }
    }
}
