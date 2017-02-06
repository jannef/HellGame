using System.Collections;
using System.Collections.Generic;
using fi.tamk.hellgame.projectiles;
using fi.tamk.hellgame.utils;
using UnityEngine;

namespace fi.tamk.hellgame.character
{
    public class BulletEmitter : MonoBehaviour
    {
        [SerializeField] protected Transform BulletOrigin;
        [SerializeField] protected LayerMask FireAtWhichLayer;

        [SerializeField] protected float Cooldown;
        [SerializeField, Range(0f, 360f)] protected float StartAngle;
        [SerializeField, Range(0f, 360f)] protected float Spread;
        [SerializeField, Range(1, 1000)] protected int NumberOfBullets;
        [SerializeField, Range(0f, 360f)] protected float Dispersion;

        protected ParticleBulletSystem BulletSystem;
        protected float Timer;

        protected Vector3 GunVector
        {
            get
            {
                return (BulletOrigin.position - transform.position).normalized;
            }
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
            BulletOrigin.transform.RotateAround(transform.position, -transform.up, Spread / 2);

            for (var i = 0; i < NumberOfBullets; i++)
            {
                FireBullet(GunVector, false);
                BulletOrigin.transform.RotateAround(transform.position, transform.up, Spread / NumberOfBullets);
            }

            BulletOrigin.transform.position = startPos;
        }

        protected void Awake()
        {
            BulletSystem = GetComponentInChildren<ParticleBulletSystem>();
            Timer = Cooldown + 1f;
            if (BulletSystem != null) BulletSystem.SetCollisionLayer(FireAtWhichLayer);
        }

        protected void Update()
        {
            Timer += Time.deltaTime;
        }

        public virtual void Fire()
        {
            if (!(Timer > Cooldown)) return;
            FireBullets(GunVector);
            Timer = 0f;
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
