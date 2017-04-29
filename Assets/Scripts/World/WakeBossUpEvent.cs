using fi.tamk.hellgame.utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace fi.tamk.hellgame.world
{
    public class WakeBossUpEvent : MonoBehaviour
    {
        public UnityEvent BossWakeUp;
        public UnityEvent BossStartActive;
        [SerializeField] private float DelayToActivation;
        private bool hasActivated = false;

        public void WakeUp()
        {
            if (hasActivated) return;
            if (BossWakeUp != null) BossWakeUp.Invoke();
            hasActivated = true;

            if (DelayToActivation <= 0)
            {
                Activate();
            }
            else
            {
                StartCoroutine(StaticCoroutines.DoAfterDelay(DelayToActivation, Activate));
            }
            
        }

        public void Activate()
        {
            if (BossStartActive != null) BossStartActive.Invoke();
        }
    }
}
