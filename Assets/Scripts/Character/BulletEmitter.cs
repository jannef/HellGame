using System.Collections;
using System.Collections.Generic;
using fi.tamk.hellgame.projectiles;
using fi.tamk.hellgame.utils;
using fi.tamk.hellgame.utils.Stairs.Utils;
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
        private float _timer;

        protected Vector3 GunVector
        {
            get
            {
                return (BulletOrigin.position - transform.position).normalized;
            }
        }

        protected void FireBullet(Vector3 trajectory, bool shotgunMode = true)
        {
              BulletSystem.EmitBullet(BulletOrigin.position, trajectory);

//            if (whichBulletIndex >= BulletPrefabs.Length) return;
//            trajectory = Quaternion.Euler(0, Random.Range(0f, Dispersion), 0) * trajectory;
//
//            var go = Pool.Instance.GetObject(BulletPrefabs[whichBulletIndex]);
//            go.transform.position = shotgunMode ? BulletOrigin.position : transform.position + trajectory;
//            go.transform.LookAt(go.transform.position + trajectory);
//            go.SetLayer(FireAtWhichLayer);
        }

        protected void FireBullets(Vector3 tra)
        {
            var startPos = BulletOrigin.transform.position;

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
            _timer = Cooldown + 1f;
            if (BulletSystem != null) BulletSystem.SetCollisionLayer(FireAtWhichLayer);
        }

        protected void Update()
        {
            _timer += Time.deltaTime;
        }

        public virtual void Fire()
        {
            if (_timer > Cooldown)
            {
                FireBullets(GunVector);
                _timer = 0f;
            }
        }
    }
}
