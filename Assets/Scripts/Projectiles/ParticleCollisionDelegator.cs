using System;
using UnityEngine;

namespace fi.tamk.hellgame.projectiles
{
    public class ParticleCollisionDelegator : MonoBehaviour
    {
        public event Action<GameObject> OnProjectileCol;

        private void OnParticleCollision(GameObject other)
        {
            if (OnProjectileCol != null) OnProjectileCol.Invoke(other);
        }
    }
}
