using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.states;
using fi.tamk.hellgame.utils;
using UnityEngine;

namespace fi.tamk.hellgame.character
{
    public class AirDropInitializer : StateInitializer
    {
        [SerializeField] protected LayerMask DropToLayer;
        [SerializeField] protected float OffsetFromGround;
        [SerializeField] protected AnimationCurve FallingCurve;
        [SerializeField, Range(0.1f, 10f)] protected float FallingDuration;      
        [SerializeField] protected float IndicatorOffsetFromGround;

        protected override void Awake()
        {
            if (RegisterAsPlayer) ServiceLocator.Instance.RegisterPlayer(gameObject);
            if (CameraWeight > 0f) ServiceLocator.Instance.MainCameraScript.AddInterest(new CameraInterest(transform, CameraWeight));
        }

        public void StartDropping()
        {
            var hc = GetComponent<ActorComponent>();

            var landing = GetLandingCoordinates();

            IInputState state;
            StateFromStateId(InitialState, out state, hc);
            IInputState airDeployState = new AirDeploymentState(hc, state, transform.position, FallingDuration,
                 FallingCurve, landing + Vector3.up * OffsetFromGround);

            hc.InitializeStateMachine(airDeployState);

            var go = Pool.Instance.GetObject(Pool.IndicatorPrefab);
            go.transform.position = landing + Vector3.up * OffsetFromGround;

            ((AirDeploymentState)airDeployState).ExitStateSignal += () => Pool.Instance.ReturnObject(ref go);
        }

        private Vector3 GetLandingCoordinates()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 1000f, DropToLayer) )
            {
                return hit.point;
            }
            else
            {
                throw new UnityException("Object attempted to be spawned above void or more than 1000f above the surface.");
            }            
        }
    }
}
