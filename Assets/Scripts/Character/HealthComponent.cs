using UnityEngine;
using UnityEngine.Events;
using System;
using fi.tamk.hellgame.utils;
using System.Collections;
using System.Collections.Generic;
using fi.tamk.hellgame.world;

namespace fi.tamk.hellgame.character
{
    public delegate bool TakeDamageDelegate(int howMuch, ref int health, ref bool flinch);

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
        public bool ReturnToPoolAfterDeath = true;

        [HideInInspector] public float InvulnerabilityTimeLeft = 0f;

        public event ReportHealthChangeDelegate HealthChangeEvent;
        public int MaxHp;

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


        public bool HasDied = false;
        private bool _hasBeenHitThisFrame = false;

        protected void Awake()
        {
            MaxHp = Health;
            ActorComponent = GetComponent<ActorComponent>();
            Pool.Instance.AddHealthComponent(gameObject, this);
        }

        public void Heal(int howMuch)
        {
            Health = Math.Min(Health + howMuch, MaxHp);
            if (HealthChangeEvent != null) HealthChangeEvent.Invoke((float)Health / (float)MaxHp, Health, MaxHp);
        }

        public void TakeDamage(int howMuch)
        {
            if (InvulnerabilityTimeLeft > 0 || HasDied) return;

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

            if (flinch && !_hasBeenHitThisFrame && !HasDied) FlinchFromHit();
            if (hp > Health) InvulnerabilityTimeLeft = InvulnerabilityLenght;

            // Report health change to subscribers of this event
            // such as UI and boss logic etc...
            if (HealthChangeEvent != null) HealthChangeEvent.Invoke((float)Health/(float)MaxHp, Health, MaxHp);
        }

        public void TakeDisplacingDamage(int howMuch) {
            TakeDamage(howMuch);            
        }

        public virtual void Die()
        {
            HasDied = true;
            if (DeathEffect != null) DeathEffect.Invoke();

            var be = GetComponentsInChildren<BulletEmitter>();
            foreach (var b in be)
            {
                b.DetachBulletEmitter(b.transform.localScale);
            }

            if (ReturnToPoolAfterDeath)
            {
                var go = gameObject;
                Pool.Instance.ReturnObject(ref go, true);
            }
        }

        public virtual void FlinchFromHit()
        {
            if (HasDied) return;

            _hasBeenHitThisFrame = true;
            StartCoroutine(FlipFlinchBooleanAtEndOfFrame());

            if (HitFlinchEffect != null)
            {
                HitFlinchEffect.Invoke();
            }
        }

        public void ActivateInvulnerability(float invulnerabilityLenght)
        {
            InvulnerabilityTimeLeft = invulnerabilityLenght;
        }

        protected void Update()
        {
            InvulnerabilityTimeLeft -= WorldStateMachine.Instance.DeltaTime;
        }

        private IEnumerator FlipFlinchBooleanAtEndOfFrame()
        {
            yield return new WaitForEndOfFrame();
            _hasBeenHitThisFrame = false;
        }

        private void OnDestroy()
        {
            if (SceneLoadLock.SceneChangeInProgress) return;

            if (!Pool.Quitting) Pool.Instance.RemoveHealthComponent(gameObject);
        }

        public void LevelCompletingDeath()
        {
            ServiceLocator.Instance.RoomBeaten = true;

            var doNotDestroy = ServiceLocator.Instance.GetAllPlayerGameObjects();
            var all = Pool.Instance.GetAllHealthComponents();

            var toDestroy = new Stack<HealthComponent>();

            foreach (HealthComponent a in all)
            {
                a.enabled = true;
                var destroy = true;
                foreach (var b in doNotDestroy)
                {
                    destroy = b.GetHashCode() == a.gameObject.GetHashCode();
                    if (!destroy)
                    {
                        break;
                    }
                }

                if (!destroy)
                {
                    if (!a.HasDied) toDestroy.Push(a);
                }
            }

            Pool.Instance.DestroyStackOfActors(toDestroy);
        }
    }
}
