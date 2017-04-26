using UnityEngine;
using System.Collections;

namespace fi.tamk.hellgame.dataholders
{
    public class StopBullets : AdvancedBulletBehavior
    {
        public override void Action(ref ParticleSystem.Particle particle)
        {
            particle.velocity *= 0;
            particle.angularVelocity3D = Vector3.zero;
            particle.startLifetime = 5f;
            particle.remainingLifetime = 5f;
        }
    }
}
