using System.Collections;
using System.Collections.Generic;
using fi.tamk.hellgame.effectors;
using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.states;
using UnityEngine;

namespace fi.tamk.hellgame.character
{
    public class AirDropInitializer : StateInitializer
    {
        [SerializeField] public Transform LandingCoordinates;
        [SerializeField] protected AnimationCurve FallingCurve;
        [SerializeField, Range(0.1f, 10f)] public float FallingDuration;

        protected override void InitializeState()
        {
            LandingCoordinates.parent = null;
            var hc = GetComponent<ActorComponent>();

            IInputState state;
            StateFromStateId(InitialState, out state, hc);
            IInputState tmp = new AirDeploymentState(hc, state, transform.position, FallingDuration, 
                 FallingCurve, LandingCoordinates.position);
            GetComponent<AerialAttackIndicatorEffector>().Activate();

            hc.InitializeStateMachine(tmp);
        }
    }
}
