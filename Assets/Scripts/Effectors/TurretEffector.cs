using fi.tamk.hellgame.utils;
using System;
using UnityEngine;

namespace fi.tamk.hellgame.effector
{
    public class TurretEffector : Effector
    {
        [SerializeField] private GameObject _turretExplosionPrefab;
        [SerializeField] private bool _usePooling = false;
        [FMODUnity.EventRef]
        public String SoundEventReference = "";

        public override void Activate()
        {
            base.Activate();
            ActivateParticleEffect();
            Utilities.PlayOneShotSound(SoundEventReference, transform.position);
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
