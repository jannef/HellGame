using fi.tamk.hellgame.utils;
using System.Collections.Generic;
using System.Reflection;
using fi.tamk.hellgame.dataholders;
using fi.tamk.hellgame.world;
using UnityEngine;

namespace fi.tamk.hellgame.projectiles
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ParticleBulletSystem : ControllableParticleSystem
    {

        public int Damage = 1;
        public float Speed = 10f;

        private void OnParticleCollision(GameObject other)
        {
            ControlledParticleSystem.GetCollisionEvents(other, CollisionEvents);

            var hc = Pool.Instance.GetHealthComponent(other.gameObject);
            if (hc == null) return;

            foreach (var e in CollisionEvents)
            {
                hc.TakeDamage(Damage);
            }
        }

        public void EmitBullet(Vector3 from, Vector3 velocity, Vector3 rotation)
        {
            var emissionParams = new ParticleSystem.EmitParams
            {
                position = from,
                velocity = velocity.normalized*Speed
            };

            if (IsBillboard)
            {
                var angle = Mathf.Atan2(velocity.x, velocity.z) * Mathf.Rad2Deg;
                emissionParams.rotation = angle;
            }
            else
            {
                emissionParams.rotation3D = rotation;
            }

            ControlledParticleSystem.Emit(emissionParams, 1);
        }

        public void SetCollisionLayer(LayerMask maskToSet)
        {
            // Fuck Unity
            var methdod = typeof(ParticleSystem.CollisionModule).GetMethod("SetCollidesWith",
                    BindingFlags.NonPublic | BindingFlags.Static);
            methdod.Invoke(null, new object[] {ControlledParticleSystem, (int)maskToSet});
        }
    }
}