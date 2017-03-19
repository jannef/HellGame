using UnityEngine;

namespace fi.tamk.hellgame.dataholders
{
    public abstract class AdvancedBulletBehavior : ScriptableObject
    {
        protected float DeltaTime = 0f;

        /// <summary>
        /// Handles per frame behaviour of EACH PARTICLE in a system.
        /// </summary>
        /// <param name="particle">Reference to the particle being affected</param>
        public virtual void Action(ref ParticleSystem.Particle particle)
        {
            // Shit can get very real when doing stuff inside this method. It is called for EACH particle during EACH FRAME of affected systems.
            // For reference consult: https://msdn.microsoft.com/en-us/library/ms973852.aspx about what you should avoid doing...
        }

        public virtual void BatchedAction(ref ParticleSystem.Particle[] particleBuffer, int numberOfParticles) { }

        public virtual void InitializeBatch(int maxBatchSize) { }

        /// <summary>
        /// Caches values for calculations happening during a frame.
        /// </summary>
        /// <param name="deltaTime">DeltaTime value to be used for calculation during the frame.</param>
        /// <param name="additionalParams">Possible additional parameters.</param>
        public virtual void CacheFrameData(float deltaTime, params object[] additionalParams)
        {
            DeltaTime = deltaTime;
            AdditionalParameters(additionalParams);
        }

        /// <summary>
        /// Handles additional parameters passed to CacheFrameData from outside.
        /// </summary>
        /// <param name="additionalParams">the parameters passed from the outside.</param>
        protected virtual void AdditionalParameters(params object[] additionalParams)
        {
            // Will probably be removed...
        }

        /// <summary>
        /// Releases allocated resources.
        /// </summary>
        public virtual void ReleaseResources() { }
    }
}
