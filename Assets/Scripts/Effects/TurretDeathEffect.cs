using fi.tamk.hellgame.character;
using fi.tamk.hellgame.utils.Stairs.Utils;
using fi.tamk.hellgame.world;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace fi.tamk.hellgame.effects
{
    public class TurretDeathEffect : DeathEffector
    {
        [SerializeField] private float _shakeIntensity;
        [SerializeField] private float _shakeLenght;
        [SerializeField] private GameObject _turretExplosionPrefab;

        public override GenericEffect Activate()
        {
            var de = base.Activate();
            de.SetOnstart(FreezeFrame, new float[0] { });
            ActivateParticleEffect();
            de.SetOnstart(ScreenShakeEffect, new float[2] { _shakeIntensity, _shakeLenght });
            return de;
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
