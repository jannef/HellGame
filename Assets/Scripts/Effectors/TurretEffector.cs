using fi.tamk.hellgame.utils;
using UnityEngine;

namespace fi.tamk.hellgame.effector
{
    public class TurretEffector : Effector
    {
        [SerializeField] private GameObject _turretExplosionPrefab;
        [SerializeField] private bool _usePooling = false;

        public override void Activate()
        {
            base.Activate();
            ActivateParticleEffect();
        }

        public void ActivateParticleEffect()
        {
            if (_turretExplosionPrefab != null)
            {
                var go = _usePooling ? Pool.Instance.GetObject(_turretExplosionPrefab) : Instantiate(_turretExplosionPrefab);
                go.transform.position = transform.position;
            }
        }
    }
}
