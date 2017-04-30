using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using fi.tamk.hellgame.dataholders;
using fi.tamk.hellgame.utils;
using fi.tamk.hellgame.world;

namespace fi.tamk.hellgame.projectiles
{
    public class ControllableParticleSystem : MonoBehaviour
    {
        public delegate void HandleOneParticle(ref ParticleSystem.Particle particle);

        [SerializeField] protected AdvancedBulletBehavior[] AdvancedBehavior;
        [SerializeField] protected bool IsBillboard = true;

        protected ParticleSystem ControlledParticleSystem;
        protected ParticleSystem.Particle[] Particles;
        protected List<ParticleCollisionEvent> CollisionEvents = new List<ParticleCollisionEvent>();

        protected void LateUpdate()
        {
            if (AdvancedBehavior.Length > 1) return;
            var numberOfParticles = ControlledParticleSystem.GetParticles(Particles);
            foreach (var behavior in AdvancedBehavior)
            {
                // Skip behaviours that are only ment to be fired "manually" instead of
                // on firing on each frame. Such as stopping all bullets momentarily...
                if (!behavior.ApplyEveryFrame) continue;
                ApplyBehavior(behavior, numberOfParticles);
            }
            ControlledParticleSystem.SetParticles(Particles, numberOfParticles);
        }

        protected void ApplyBehavior(AdvancedBulletBehavior behavior, int numberOfBullets, params object[] parameters)
        {
            var gpuBehavior = behavior as GpuAcceleratedBulletBehavior;
            if (gpuBehavior == null)
            {
                behavior.CacheFrameData(WorldStateMachine.Instance.DeltaTime);
                ParticleManipulationLoop(ref Particles, numberOfBullets, behavior.Action);
            }
            else
            {
                gpuBehavior.BatchedAction(ref Particles, numberOfBullets, parameters);
            }
        }

        public void OneshotBehaviour(int index, bool suppressWarnings, params object[] parameters)
        {
            if (AdvancedBehavior.Length <= index || index < 0)
            {
                throw new UnityException(string.Format("OneshotBehaviour out of range ( [0,{0}] ) with given index: {1}", AdvancedBehavior.Length - 1, index));
            }
            if (!suppressWarnings && AdvancedBehavior[index].ApplyEveryFrame) Debug.LogWarning("OneshotBehaviour called for continuously called behaviour!");

            var numberOfBullets = ControlledParticleSystem.GetParticles(Particles);
            ApplyBehavior(AdvancedBehavior[index], numberOfBullets, parameters);
            ControlledParticleSystem.SetParticles(Particles, numberOfBullets);
        }

        /// <summary>
        /// This is callable from UnityEvents due it's signature, unlike full OneshotBehaviour.
        /// </summary>
        /// <param name="index"></param>
        public void SimpleOneshot(int index)
        {
            OneshotBehaviour(index, true);
        }

        protected virtual void ParticleManipulationLoop(ref ParticleSystem.Particle[] particles, int numberOfParticles, ParticleBulletSystem.HandleOneParticle action)
        {
            for (var i = 0; i < numberOfParticles; ++i)
            {
                action(ref particles[i]);
            }
        }

        protected void Awake()
        {
            ControlledParticleSystem = GetComponentInChildren<ParticleSystem>();
            Particles = new ParticleSystem.Particle[ControlledParticleSystem.main.maxParticles];

            for (var index = 0; index < AdvancedBehavior.Length; index++)
            {
                AdvancedBehavior[index] = Instantiate(AdvancedBehavior[index]);
                var gpuBehavior = AdvancedBehavior[index] as GpuAcceleratedBulletBehavior;
                if (gpuBehavior != null)
                {
                    gpuBehavior.InitializeBatch(ControlledParticleSystem.main.maxParticles);
                }
            }
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
