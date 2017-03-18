using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.dataholders
{
    public sealed class CheapAcceleration : AdvancedBulletBehavior
    {
        [SerializeField] private float AccelerationPerSecond = 1f;

        private float _multiplier = 0f;

        public override void Action(ref ParticleSystem.Particle particle)
        {
            particle.velocity *= _multiplier;
        }

        public override void CacheFrameData(float deltaTime, params object[] additionalParams)
        {
            _multiplier = 1f + AccelerationPerSecond * deltaTime;
        }
    }
}
