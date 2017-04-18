using fi.tamk.hellgame.utils;
using System.Collections.Generic;
using System.Reflection;
using fi.tamk.hellgame.dataholders;
using fi.tamk.hellgame.world;
using UnityEngine;

namespace fi.tamk.hellgame.projectiles
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ParticleBulletSystem : MonoBehaviour
    {
        public delegate void HandleOneParticle(ref ParticleSystem.Particle particle);

        [SerializeField] protected AdvancedBulletBehavior[] AdvancedBehavior;
        [SerializeField] protected bool IsBillboard = true;

        public int Damage = 1;
        public float Speed = 10f;

        protected ParticleSystem BulletSystem;
        protected ParticleSystem.Particle[] Bullets;

        protected List<ParticleCollisionEvent> CollisionEvents = new List<ParticleCollisionEvent>();

        private void OnParticleCollision(GameObject other)
        {
            BulletSystem.GetCollisionEvents(other, CollisionEvents);

            var hc = Pool.Instance.GetHealthComponent(other.gameObject);
            if (hc == null) return;

            foreach (var e in CollisionEvents)
            {
                hc.TakeDamage(Damage);
            }
        }

        protected void LateUpdate()
        {
            if (AdvancedBehavior.Length > 1) return;
            var numberOfBullets = BulletSystem.GetParticles(Bullets);
            foreach (var behavior in AdvancedBehavior)
            {
                // Skip behaviours that are only ment to be fired "manually" instead of
                // on firing on each frame. Such as stopping all bullets momentarily...
                if (!behavior.ApplyEveryFrame) continue;
                ApplyBehavior(behavior, numberOfBullets);
            }
            BulletSystem.SetParticles(Bullets, numberOfBullets);
        }

        protected void ApplyBehavior(AdvancedBulletBehavior behavior, int numberOfBullets, params object[] parameters)
        {
            var gpuBehavior = behavior as GpuAcceleratedBulletBehavior;
            if (gpuBehavior == null)
            {
                behavior.CacheFrameData(WorldStateMachine.Instance.DeltaTime);
                ParticleManipulationLoop(ref Bullets, numberOfBullets, behavior.Action);
            }
            else
            {
                gpuBehavior.BatchedAction(ref Bullets, numberOfBullets, parameters);
            }
        }

        public void OneshotBehaviour(int index, bool suppressWarnings, params object[] parameters)
        {
            if (AdvancedBehavior.Length <= index || index < 0)
            {
                throw new UnityException(string.Format("OneshotBehaviour out of range ( [0,{0}] ) with given index: {1}",AdvancedBehavior.Length - 1, index));
            }
            if (!suppressWarnings && AdvancedBehavior[index].ApplyEveryFrame) Debug.LogWarning("OneshotBehaviour called for continuously called behaviour!");

            var numberOfBullets = BulletSystem.GetParticles(Bullets);
            ApplyBehavior(AdvancedBehavior[index], numberOfBullets, parameters);
            BulletSystem.SetParticles(Bullets, numberOfBullets);
        }

        protected virtual void ParticleManipulationLoop(ref ParticleSystem.Particle[] particles, int numberOfParticles, HandleOneParticle action)
        {
            for (var i = 0; i < numberOfParticles; ++i)
            {
                action(ref particles[i]);
            }    
        }

        protected void Awake()
        {
            BulletSystem = GetComponentInChildren<ParticleSystem>();
            Bullets = new ParticleSystem.Particle[BulletSystem.main.maxParticles];

            for (var index = 0; index < AdvancedBehavior.Length; index++)
            {
                AdvancedBehavior[index] = Instantiate(AdvancedBehavior[index]);
                var gpuBehavior = AdvancedBehavior[index] as GpuAcceleratedBulletBehavior;
                if (gpuBehavior != null)
                {
                    gpuBehavior.InitializeBatch(BulletSystem.main.maxParticles);
                }
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

            BulletSystem.Emit(emissionParams, 1);
        }

        public void SetCollisionLayer(LayerMask maskToSet)
        {
            // Fuck Unity
            var methdod = typeof(ParticleSystem.CollisionModule).GetMethod("SetCollidesWith",
                    BindingFlags.NonPublic | BindingFlags.Static);
            methdod.Invoke(null, new object[] {BulletSystem, (int)maskToSet});
        }

        public void OnDestroy()
        {
            foreach (var behavior in AdvancedBehavior)
            {
                behavior.ReleaseResources();
                DestroyImmediate(behavior);
            }
        }
    }
}