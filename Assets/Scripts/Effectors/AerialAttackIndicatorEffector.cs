using fi.tamk.hellgame.character;
using fi.tamk.hellgame.effector;
using fi.tamk.hellgame.utils;
using fi.tamk.hellgame.world;
using UnityEngine;

namespace fi.tamk.hellgame.effectors
{
    [RequireComponent(typeof(AirDropInitializer))]
    public class AerialAttackIndicatorEffector : Effector
    {
        [SerializeField] protected GameObject ProjectorPrefab;
        [SerializeField] protected float ImpactScreenShakeIntensity;
        [SerializeField] protected float ImpactScreenShakeLenght;
        [SerializeField] protected float ProjectionMinsize;
        [SerializeField] protected float ProjectionMaxSize;

        protected AirDropInitializer AirDropInitializer = null;

        private GameObject _effectGameObject;
        private Projector _landindProjector;
        private float _time = 0f;

        public override void Activate()
        {
            base.Activate();
            AirDropInitializer = GetComponent<AirDropInitializer>();
            _effectGameObject = Instantiate(ProjectorPrefab, Effect.transform);
            _effectGameObject.transform.localPosition = Vector3.zero;
            _landindProjector = _effectGameObject.GetComponent<Projector>();

            Effect.SetOnUpdateCycle(FollowAnother, new float[]{0});
            Effect.SetOnEnd(ScreenShakeEffect, new float[] { ImpactScreenShakeIntensity, ImpactScreenShakeLenght });

            if (AirDropInitializer == null) return;
            Effect.LifeTime = AirDropInitializer.FallingDuration;
        }

        public void FollowAnother(float[] args)
        {
            if (ServiceLocator.Instance.RoomBeaten) return; // If the this effector is destroyed, return.

            Effect.transform.position = AirDropInitializer.transform.position;
            _effectGameObject.transform.forward = AirDropInitializer.LandingCoordinates.position - transform.position;
            _time += WorldStateMachine.Instance.DeltaTime / Effect.LifeTime;
            _landindProjector.orthographicSize = Mathf.Lerp(ProjectionMinsize, ProjectionMaxSize, AirDropInitializer.FallingCurve.Evaluate(_time));
        }
    }
}
