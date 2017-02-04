using fi.tamk.hellgame.character;
using fi.tamk.hellgame.effector;
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

        private GameObject EffectGameObject;
        private Projector _landindProjector;
        private float _time = 0f;

        public override void Activate()
        {
            base.Activate();
            AirDropInitializer = GetComponent<AirDropInitializer>();
            EffectGameObject = Instantiate(ProjectorPrefab, Effect.transform);
            EffectGameObject.transform.localPosition = Vector3.zero;
            _landindProjector = EffectGameObject.GetComponent<Projector>();
            

            Effect.SetOnUpdateCycle(FollowAnother, new float[]{0});
            Effect.SetOnEnd(ScreenShakeEffect, new float[2] { ImpactScreenShakeIntensity, ImpactScreenShakeLenght });

            if (AirDropInitializer == null) return;
            Effect.LifeTime = AirDropInitializer.FallingDuration;
        }

        public void FollowAnother(float[] args)
        {
            Effect.transform.position = AirDropInitializer.transform.position;
            EffectGameObject.transform.forward = AirDropInitializer.LandingCoordinates.position - transform.position;
            _time += Time.deltaTime / Effect.LifeTime;
            _landindProjector.orthographicSize = Mathf.Lerp(ProjectionMinsize, ProjectionMaxSize, AirDropInitializer.FallingCurve.Evaluate(_time));
        }
    }
}
