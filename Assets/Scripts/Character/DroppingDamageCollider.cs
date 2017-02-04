using fi.tamk.hellgame.effector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.character
{
    public class DroppingDamageCollider : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            var hc = other.gameObject.GetComponent<HealthComponent>();
            if (hc == null) return;
            hc.TakeDisplacingDamage(1);
        }

        public void SelfDestruct(float[] args)
        {
            Destroy(gameObject);
        }
    }
}
