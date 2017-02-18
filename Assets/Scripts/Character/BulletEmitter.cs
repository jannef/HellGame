using System.Collections;
using System.Collections.Generic;
using fi.tamk.hellgame.projectiles;
using fi.tamk.hellgame.utils;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace fi.tamk.hellgame.character
{
    public class BulletEmitter : MonoBehaviour
    {
        public UnityEvent FiringEvent;

        [SerializeField] protected Transform BulletOrigin;
        [SerializeField] protected LayerMask FireAtWhichLayer;

        [SerializeField] protected float Cooldown;
        [SerializeField, Range(0f, 360f)] protected float StartAngle;
        [SerializeField, Range(0f, 360f)] protected float Spread;
        [SerializeField, Range(1, 1000)] protected int NumberOfBullets;
        [SerializeField, Range(0f, 360f)] protected float Dispersion;
        
        protected ParticleBulletSystem BulletSystem;
        protected float Timer;

        protected float DefaultCooldown;
        protected float DefaultSpread;
        protected int DefaultNumberOfBullets;
        protected float DefaultDispersion;
        protected float DefaultBulletSpeed;
        protected int DefaultDamage;

        protected Vector3 GunVector
        {
            get { return (BulletOrigin.position - transform.position).normalized; }
        }

        protected void FireBullet(Vector3 trajectory, bool shotgunMode = true)
        {
            if (Dispersion > 0)
            {
                trajectory = Quaternion.Euler(0, Random.Range(-Dispersion / 2, Dispersion / 2), 0) * trajectory;
            }
            BulletSystem.EmitBullet(BulletOrigin.position, trajectory);
        }

        protected void FireBullets(Vector3 tra)
        {
            var startPos = BulletOrigin.transform.position;
            BulletOrigin.RotateAround(transform.position, -transform.up, Spread / 2);

            for (var i = 0; i < NumberOfBullets; i++)
            {
                FireBullet(GunVector, false);
                BulletOrigin.transform.RotateAround(transform.position, transform.up, Spread / NumberOfBullets);
            }

            BulletOrigin.transform.position = startPos;
        }

        protected void RestoreStatsToDefault()
        {
            Cooldown = DefaultCooldown;
            Dispersion = DefaultDispersion;
            Spread = DefaultSpread;
            NumberOfBullets = DefaultNumberOfBullets;
            Cooldown = DefaultCooldown;

            BulletSystem.Speed = DefaultBulletSpeed;
            BulletSystem.Damage = DefaultDamage;
        }

        protected virtual void Awake()
        {
            BulletSystem = GetComponentInChildren<ParticleBulletSystem>();
            Timer = Cooldown + 1f;
            if (BulletSystem == null) throw new AssertionException("Bullet system not found for this gun: " + gameObject , "");

            BulletSystem.SetCollisionLayer(FireAtWhichLayer);

            DefaultCooldown = Cooldown;
            DefaultDispersion = Dispersion;
            DefaultNumberOfBullets = NumberOfBullets;
            DefaultSpread = Spread;
            DefaultDamage = BulletSystem.Damage;
            DefaultBulletSpeed = BulletSystem.Speed;
        }

        protected virtual void Update()
        {
            Timer += Time.deltaTime;
        }

        public virtual void Fire()
        {
            if (!(Timer > Cooldown)) return;
            FireBullets(GunVector);
            Timer = 0f;
            if (FiringEvent != null) FiringEvent.Invoke();
        }

        public void DetachBulletEmitter(Vector3 localScale)
        {
            // Apparently not replicating the scale will fuck up the particle system when it detaches from its
            // parent...
            BulletSystem.gameObject.transform.SetParent(null);
            BulletSystem.gameObject.AddComponent<ParticleSystemCleaner>();
            BulletSystem.gameObject.transform.localScale = localScale;
        }
    }
}
