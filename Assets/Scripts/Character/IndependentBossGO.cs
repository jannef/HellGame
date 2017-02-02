using fi.tamk.hellgame.states;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.character
{
    public class IndependentBossGO : MonoBehaviour {
        private StateBossRoutine _attachedState;

        public virtual void AddToState(StateBossRoutine routine)
        {
            _attachedState = routine;
            routine.EnabledEvent += Enable;
            routine.DisabledEvent += Disable;
        }

        public virtual void Enable(object sender)
        {
            if (gameObject != null) gameObject.SetActive(true);
            
        }

        public virtual void Disable(object sender)
        {
            if (gameObject != null) gameObject.SetActive(false);
        }

        void OnDestroy()
        {
            _attachedState.DisabledEvent -= Disable;
            _attachedState.EnabledEvent -= Enable;
        }
    }
}
