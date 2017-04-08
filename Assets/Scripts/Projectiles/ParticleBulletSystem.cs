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
        [SerializeField] protected float[] RuntimeConfigFloats;
        [SerializeField] protected GameObject BulletSystemPrefab;

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
                var gpuBehavior = behavior as GpuAcceleratedBulletBehavior;
                if (gpuBehavior == null)
                {
                    behavior.CacheFrameData(WorldStateMachine.Instance.DeltaTime);
                    ParticleManipulationLoop(ref Bullets, numberOfBullets, behavior.Action);
                }
                else
                {
                    gpuBehavior.BatchedAction(ref Bullets, numberOfBullets);
                }
            }
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
            if (BulletSystemPrefab != null)
            {
                Instantiate(BulletSystemPrefab, transform).GetComponent<ParticleCollisionDelegator>().OnProjectileCol += OnParticleCollision;
            }
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

        public void EmitBullet(Vector3 from, Vector3 velocity)
        {
            var emissionParams = new ParticleSystem.EmitParams
            {
                position = from,
                velocity = velocity.normalized*Speed
            };
            var angle = Mathf.Atan2(velocity.x, velocity.z) * Mathf.Rad2Deg;
            emissionParams.rotation = angle;

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