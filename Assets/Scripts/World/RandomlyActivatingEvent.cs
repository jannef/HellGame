using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace fi.tamk.hellgame.world
{

    public class RandomlyActivatingEvent : MonoBehaviour
    {
        [SerializeField, Range(0.001f, 1)] private float ActivationChance;
        public UnityEvent ActivationEvent;

        public void RollToActivate()
        {
            if (Random.value <= ActivationChance)
            {
                if (ActivationEvent != null) ActivationEvent.Invoke();
                ActivationEvent = null;
            }
        }
    }
}
