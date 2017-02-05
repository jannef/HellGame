using fi.tamk.hellgame.utils.Stairs.Utils;
using UnityEngine;
using UnityEngine.Events;
using System;
using fi.tamk.hellgame.utils;

namespace fi.tamk.hellgame.character
{
    public delegate bool TakeDamageDelegate(int howMuch, ref int health, ref bool flinch);
    public delegate void TeleportDelegate(Vector3 targetPosition);

    public delegate void ReportHealthChangeDelegate(float percentage, int currentHp, int maxHp);

    public class HealthComponent : MonoBehaviour
    {
        public int Health = 1;
        public int Armour = 0;
        public float InvulnerabilityLenght = 0;

        /// <summary>
        /// If this is set to false, character will take damage, but won't die to it as long as it remains
        /// false.
        ///
        /// Some enemies use this to perform final attack sequences before being removed etc. It's left at
        /// the resposibility of that class to set this back to true and kill itselff off.
        /// </summary>
        public bool AllowDeath = true;

        [HideInInspector] public float InvulnerabilityTimeLeft = 0f;

        public event ReportHealthChangeDelegate HealthChangeEvent;
        public int MaxHp;
        public bool TeleportToSafetyAfterDisplacingHit;

        protected TakeDamageDelegate DamageDelegate
        {
            get
            {
                return ActorComponent.TakeDamage;
            }
        }

        [SerializeField] public UnityEvent DeathEffect;
        [SerializeField] public UnityEvent HitFlinchEffect;
        protected ActorComponent ActorComponent;
        protected TeleportDelegate TeleportToSafety
        {
            get
            {
                return ActorComponent.Teleport;
            }
        }

        protected void Awake()
        {
            MaxHp = Health;
            ActorComponent = GetComponent<ActorComponent>();
            Pool.Instance.GameObjectToHealth.Add(gameObject, this);
        }

        public void TakeDamage(int howMuch)
        {
            if (InvulnerabilityTimeLeft > 0) return;

            var hp = Health;
            var flinch = false;
            howMuch = Math.Max(howMuch - Armour, 0);
            if (DamageDelegate == null) return;

            // Do not optimize the order of this comparison, it will make the actor immune to
            // damage as well as not being able to die to it!
            if (!DamageDelegate(howMuch, ref Health, ref flinch) && AllowDeath)
            {
                Die();
                return;
            }

            if (flinch) FlinchFromHit();
            if (hp > Health) InvulnerabilityTimeLeft = InvulnerabilityLenght;

            // Report health change to subscribers of this event
            // such as UI and boss logic etc...
            if (HealthChangeEvent != null) HealthChangeEvent.Invoke((float)Health/(float)MaxHp, Health, MaxHp);
        }

        public void TakeDisplacingDamage(int howMuch) {
            if (TeleportToSafetyAfterDisplacingHit)
            {
                TakeDamage(howMuch);

                if (Health > 0)
                {
                    if (TeleportToSafety != null)
                    {
                        //TODO: Have Respawnpoints check for spawn safety
                        TeleportToSafety(ServiceLocator.Instance.GetNearestRespawnPoint(transform.position).transform.position);
                    }
                }
            } else
            {
                Die();
            }
            
        }

        public virtual void Die()
        {

            if (DeathEffect != null)
            {
                DeathEffect.Invoke();
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
            if (HitFlinchEffect != null)
            {
                HitFlinchEffect.Invoke();
            }
        }

        protected void Update()
        {
            InvulnerabilityTimeLeft -= Time.deltaTime;
        }
    }
}
