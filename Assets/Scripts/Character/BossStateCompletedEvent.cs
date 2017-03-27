using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace fi.tamk.hellgame.character
{

    public class BossStateCompletedEvent : MonoBehaviour
    {
        public UnityEvent PhaseCompletedEvent;
        public UnityEvent TransitionPhaseEnd;

        public void PhaseCompleted()
        {
            if (PhaseCompletedEvent != null) PhaseCompletedEvent.Invoke();
        }

        public void TransitionPhaseCompleted()
        {
            if (TransitionPhaseEnd != null) TransitionPhaseEnd.Invoke();
        }


    }
}
