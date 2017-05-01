using UnityEngine;

namespace fi.tamk.hellgame.character
{
    public class BelurkerLaserEmitter : LaserEmitter
    {
        protected override void SetEndParticles(Vector3[] laserPositions)
        {
            base.SetEndParticles(laserPositions);
            _endPointParticle.transform.LookAt(laserPositions[0]);
        }
    }
}
