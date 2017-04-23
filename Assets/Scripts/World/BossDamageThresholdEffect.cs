using fi.tamk.hellgame.utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace fi.tamk.hellgame.world
{
    public class BossDamageThresholdEffect : MonoBehaviour
    {
        public UnityEvent ActivationEvent;
        public UnityEvent WakeUpEvent;
        [HideInInspector] public float Priority = 1f;
        [HideInInspector] public bool HasActivated = false;

        public void Initialize()
        {
            Priority = Random.Range(0f, 1f);
        }

        public void Wake()
        {
            if (WakeUpEvent != null) WakeUpEvent.Invoke();
        }

        public void Pop()
        {
            if (ActivationEvent != null) ActivationEvent.Invoke();
        }
    }
}
