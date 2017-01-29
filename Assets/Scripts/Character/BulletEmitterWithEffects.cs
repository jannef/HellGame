using fi.tamk.hellgame.character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.character
{
    [RequireComponent(typeof(DeathEffector))]
    public class BulletEmitterWithEffects : BulletEmitter
    {
        [SerializeField] protected DeathEffector _firingEffect;

        public override void Fire()
        {
            if (_timer > Cooldown)
            {
                FireBullets(GunVector);
                _timer = 0f;
                if (_firingEffect != null) _firingEffect.Activate();
            }
        }
    }
}
