using UnityEngine;

namespace fi.tamk.hellgame.effector
{
    public class TurretEffector : Effector
    {
        [SerializeField] private GameObject _turretExplosionPrefab;

        public override void Activate()
        {
            base.Activate();
            ActivateParticleEffect();
        }

        public void ActivateParticleEffect()
        {
            if (_turretExplosionPrefab != null)
            {
                var go = Instantiate(_turretExplosionPrefab);
                go.transform.position = transform.position;
            }
        }
    }
}
