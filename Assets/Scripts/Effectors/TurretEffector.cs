using fi.tamk.hellgame.character;
using fi.tamk.hellgame.utils.Stairs.Utils;
using fi.tamk.hellgame.world;
using UnityEngine;

namespace fi.tamk.hellgame.effector
{
    public class TurretEffector : Effector
    {
        [SerializeField] private float _shakeIntensity;
        [SerializeField] private float _shakeLenght;
        [SerializeField] private GameObject _turretExplosionPrefab;

        public override void Activate()
        {
            Effect.SetOnstart(FreezeFrame, new float[0] { });
            ActivateParticleEffect();
            Effect.SetOnstart(ScreenShakeEffect, new float[2] { _shakeIntensity, _shakeLenght });
        }

        public void ActivateParticleEffect()
        {
            if (_turretExplosionPrefab != null)
            {
                // TODO: Pool particle effects
                GameObject go = Instantiate(_turretExplosionPrefab);
                go.transform.position = transform.position;
            }
        }
    }
}
