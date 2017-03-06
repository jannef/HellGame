using UnityEngine;

namespace fi.tamk.hellgame.effector
{
    public class TurretEffector : Effector
    {
        [SerializeField] private float _shakeIntensity;
        [SerializeField] private float _shakeLenght;
        [SerializeField] private float _freezeFrameLenght = 20f;
        [SerializeField] private GameObject _turretExplosionPrefab;

        public override void Activate()
        {
            base.Activate();
            Effect.SetOnstart(ThreadFreezeFrame, new float[1] { _freezeFrameLenght });
            ActivateParticleEffect();
            Effect.SetOnstart(ScreenShakeEffect, new float[2] { _shakeIntensity, _shakeLenght });
        }

        public void ActivateParticleEffect()
        {
            if (_turretExplosionPrefab != null)
            {
                // TODO: Pool particle effects
                var go = Instantiate(_turretExplosionPrefab);
                go.transform.position = transform.position;
            }
        }
    }
}
