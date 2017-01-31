using fi.tamk.hellgame.character;
using fi.tamk.hellgame.effector;
using UnityEngine;

namespace fi.tamk.hellgame.effectors
{
    [RequireComponent(typeof(AirDropInitializer))]
    public class AerialAttackIndicatorEffector : Effector
    {
        [SerializeField] protected GameObject _projectorPrefab;
        protected AirDropInitializer AirDropInitializer = null;

        private GameObject EffectGameObject;

        public override void Activate()
        {
            base.Activate();
            AirDropInitializer = GetComponent<AirDropInitializer>();
            EffectGameObject = Instantiate(_projectorPrefab, Effect.transform);
            EffectGameObject.transform.localPosition = Vector3.zero;

            Effect.SetOnUpdateCycle(FollowAnother, new float[]{0});

            if (AirDropInitializer == null) return;
            Effect.LifeTime = AirDropInitializer.FallingDuration;
        }

        public void FollowAnother(float[] args)
        {
            EffectGameObject.transform.position = AirDropInitializer.transform.position;
            EffectGameObject.transform.rotation = Quaternion.Euler(AirDropInitializer.LandingCoordinates.position - transform.position);
        }
    }
}
