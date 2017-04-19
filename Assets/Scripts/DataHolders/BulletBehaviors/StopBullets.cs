using UnityEngine;
using System.Collections;

namespace fi.tamk.hellgame.dataholders
{
    public class StopBullets : AdvancedBulletBehavior
    {
        public override void Action(ref ParticleSystem.Particle particle)
        {
            particle.velocity *= 0;
        }
    }
}
