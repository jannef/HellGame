using System.Collections;
using System.Collections.Generic;
using fi.tamk.hellgame.utils.Stairs.Utils;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.Assertions.Comparers;

namespace fi.tamk.hellgame.character
{
    public delegate bool TakeDamageDelegate(int howMuch, ref int health, ref bool flinch);

    public class HealthComponent : MonoBehaviour
    {
        public int Health = 1;
        public int Armour = 0;
        public float InvulnerabilityLenght = 0;

        public float InvulnerabilityTimeLeft = 0f;

        protected TakeDamageDelegate DamageDelegate
        {
            get
            {
                return _actorComponent.TakeDamage;
            }
        }

        [SerializeField] private UnityEvent _deathEffect;
        [SerializeField] private UnityEvent _hitFlinchEffect;
        protected ActorComponent _actorComponent;

        protected void Awake()
        {
            _actorComponent = GetComponent<ActorComponent>();
            Pool.Instance.GameObjectToHealth.Add(gameObject, this);
        }

        public void TakeDamage(int howMuch)
        {
            if (InvulnerabilityTimeLeft > 0) return;

            var hp = Health;
            bool flinch = false;
            howMuch = Math.Max(howMuch - Armour, 0);
            if (DamageDelegate == null) return;
            if (!DamageDelegate(howMuch, ref Health, ref flinch))
            {
                Die();
                return;
            }

            if (flinch) FlinchFromHit();
            if (hp > Health) InvulnerabilityTimeLeft = InvulnerabilityLenght;
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

        protected void Update()
        {
            InvulnerabilityTimeLeft -= Time.deltaTime;
        }
    }
}
