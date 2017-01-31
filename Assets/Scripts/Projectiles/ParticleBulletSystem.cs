﻿using System.Collections.Generic;
using System.Reflection;
using fi.tamk.hellgame.utils.Stairs.Utils;
using UnityEngine;

namespace fi.tamk.hellgame.projectiles
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ParticleBulletSystem : MonoBehaviour
    {
        [SerializeField] protected int Damage = 1;
        [SerializeField] protected float Speed = 10f;

        protected ParticleSystem BulletSystem;
        protected ParticleSystem.Particle[] Bullets;

        protected List<ParticleCollisionEvent> CollisionEvents = new List<ParticleCollisionEvent>();

        private void OnParticleCollision(GameObject other)
        {
            BulletSystem.GetCollisionEvents(other, CollisionEvents);

            var hc = Pool.Instance.GetHero(other.gameObject);
            if (hc == null) return;

            foreach (var e in CollisionEvents)
            {
                hc.TakeDamage(Damage);
            }
        }

        protected void Awake()
        {
            BulletSystem = GetComponentInChildren<ParticleSystem>();
            Bullets = new ParticleSystem.Particle[BulletSystem.main.maxParticles];
        }

        public void EmitBullet(Vector3 from, Vector3 velocity)
        {
            var emissionParams = new ParticleSystem.EmitParams();
            emissionParams.position = from;
            emissionParams.velocity = velocity.normalized * Speed;

            BulletSystem.Emit(emissionParams, 1);
        }

        public void SetCollisionLayer(LayerMask maskToSet)
        {
            // Fuck Unity
            var methdod = typeof(ParticleSystem.CollisionModule).GetMethod("SetCollidesWith",
                    BindingFlags.NonPublic | BindingFlags.Static);
            var instance = typeof(ParticleSystem.CollisionModule).GetField("m_ParticleSystem", BindingFlags.NonPublic | BindingFlags.Instance);
            var value = instance.GetValue(BulletSystem.collision);
            methdod.Invoke(value, new object[] {value, (int)maskToSet});
        }
    }
}