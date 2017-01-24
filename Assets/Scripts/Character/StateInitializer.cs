
using System;
using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.states;
using UnityEngine;

namespace fi.tamk.hellgame.character
{
    [RequireComponent(typeof(HeroController))]
    class StateInitializer : MonoBehaviour
    {
        public InputStates InitialState;

        protected void Awake()
        {
            var hc = GetComponent<HeroController>();
            IInputState tmp = null;
            switch (InitialState)
            {
                case InputStates.Running:
                    tmp = new StateRunning(hc);
                    break;
                case InputStates.Error:
                    break;
                case InputStates.Paused:
                    tmp = new StatePaused(hc);
                    break;
                case InputStates.Dashing:
                    tmp = new StateDashing(hc, Vector3.forward);
                    break;
                case InputStates.Dead:
                    break;
                case InputStates.EnemyTurret:
                    tmp = new EnemyTurret(hc);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            hc.InitializeStateMachine(tmp);
        }
    }
}
