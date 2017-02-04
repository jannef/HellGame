using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.states;
using UnityEngine;
using UnityEngine.Events;

namespace fi.tamk.hellgame.character
{
    public class AirDropInitializer : StateInitializer
    {
        [SerializeField] public Transform LandingCoordinates;
        [SerializeField] public Transform BottomPointTransform;
        [SerializeField] public AnimationCurve FallingCurve;
        [SerializeField, Range(0.1f, 10f)] public float FallingDuration;
        public UnityEvent AirDropEffects;

        protected override void InitializeState()
        {
            LandingCoordinates.parent = null;
            var hc = GetComponent<ActorComponent>();

            IInputState state;
            StateFromStateId(InitialState, out state, hc);
            IInputState tmp = new AirDeploymentState(hc, state, transform.position, FallingDuration, 
                 FallingCurve, LandingCoordinates.position, BottomPointTransform.localPosition);
            AirDropEffects.Invoke();

            hc.InitializeStateMachine(tmp);
        }
    }
}
