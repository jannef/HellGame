using fi.tamk.hellgame.effector;
using UnityEngine;
using UnityEngine.Events;

namespace fi.tamk.hellgame.character
{
    [RequireComponent(typeof(Effector))]
    public class BulletEmitterWithEffects : BulletEmitter
    {
        [SerializeField] protected UnityEvent _firingEffect;

        public override void Fire()
        {
            if (_timer > Cooldown)
            {
                FireBullets(GunVector);
                _timer = 0f;
                if (_firingEffect != null) _firingEffect.Invoke();
            }
        }
    }
}
