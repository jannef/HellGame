using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.character
{
    public class EntryMovementInitializer : MonoBehaviour {
        [SerializeField] public AnimationCurve DeccelerationCurve;
        [SerializeField] public float StartingSpeed;
        [SerializeField] public float EndSpeed;
        [SerializeField] public float DecelerationTime;
        [SerializeField] public float Gravity;
        [SerializeField] protected InputStates InitialState;

        public void Initialize(Vector3 directionToLaunch)
        {
            var hc = GetComponent<ActorComponent>();

            IInputState state;
            StateInitializer.StateFromStateId(InitialState, out state, hc);
            IInputState tmp = new StateFlying(hc, state, DeccelerationCurve, StartingSpeed, DecelerationTime, directionToLaunch, Gravity);

            hc.InitializeStateMachine(tmp);
        }
    }
}
