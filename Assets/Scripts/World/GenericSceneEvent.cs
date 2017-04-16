using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace fi.tamk.hellgame.world
{

    public class GenericSceneEvent : MonoBehaviour
    {
        public UnityEvent Event;

        public void ActivateEvent()
        {
            if (Event != null) Event.Invoke();
        }
    }
}
