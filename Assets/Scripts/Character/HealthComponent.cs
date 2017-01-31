using System.Collections;
using System.Collections.Generic;
using fi.tamk.hellgame.utils.Stairs.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace fi.tamk.hellgame.character
{
    [RequireComponent(typeof(ActorComponent))]
    public class HealthComponent : MonoBehaviour
    {
        public float Speed = 1;
        public float DashSpeed = 10;
        public float DashDuration = 0.75f;
        public int Health = 1;
        public float InvulnerabilityLenght = 0;

        [SerializeField] private UnityEvent _deathEffect;
        [SerializeField] private UnityEvent _hitFlinchEffect;
        protected ActorComponent _actorComponent;

        protected void Awake()
        {
            _actorComponent = GetComponent<ActorComponent>();
        }

        public void TakeDamage(int howMuch)
        {
            _actorComponent.TakeDamage(howMuch);
        }

        public virtual void Die()
        {

            if (_deathEffect != null)
            {
                _deathEffect.Invoke();
            }

            var be = GetComponentsInChildren<BulletEmitter>();
            foreach (var b in be)
            {
                b.DetachBulletEmitter();
            }

            Pool.DelayedDestroyGo(gameObject);
        }

        public virtual void FlinchFromHit()
        {
            if (_hitFlinchEffect != null)
            {
                _hitFlinchEffect.Invoke();
            }
        }
    }
}
