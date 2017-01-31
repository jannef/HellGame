using fi.tamk.hellgame.character;
using fi.tamk.hellgame.effector;
using UnityEngine;

namespace fi.tamk.hellgame.effectors
{
    [RequireComponent(typeof(AirDropInitializer))]
    public class AerialAttackIndicatorEffector : Effector
    {
        [SerializeField] protected GameObject _projectorPrefab;
        [SerializeField] protected float _impactScreenShakeIntensity;
        [SerializeField] protected float _impactScreenShakeLenght;
        [SerializeField] protected float _projectionMinsize;
        [SerializeField] protected float _projectionMaxSize;

        protected AirDropInitializer AirDropInitializer = null;

        private GameObject EffectGameObject;
        private Projector _landindProjector;
        private float _time = 0f;

        public override void Activate()
        {
            base.Activate();
            AirDropInitializer = GetComponent<AirDropInitializer>();
            EffectGameObject = Instantiate(_projectorPrefab, Effect.transform);
            EffectGameObject.transform.localPosition = Vector3.zero;
            _landindProjector = EffectGameObject.GetComponent<Projector>();
            

            Effect.SetOnUpdateCycle(FollowAnother, new float[]{0});
            Effect.SetOnEnd(ScreenShakeEffect, new float[2] { _impactScreenShakeIntensity, _impactScreenShakeLenght });

            if (AirDropInitializer == null) return;
            Effect.LifeTime = AirDropInitializer.FallingDuration;
        }

        public void FollowAnother(float[] args)
        {
            Effect.transform.position = AirDropInitializer.transform.position;
            EffectGameObject.transform.forward = AirDropInitializer.LandingCoordinates.position - transform.position;
            _time += Time.deltaTime / Effect.LifeTime;
            Debug.Log(Mathf.Lerp(_projectionMinsize, _projectionMaxSize, AirDropInitializer.FallingCurve.Evaluate(_time)));
            _landindProjector.orthographicSize = Mathf.Lerp(_projectionMinsize, _projectionMaxSize, AirDropInitializer.FallingCurve.Evaluate(_time));
        }
    }
}
