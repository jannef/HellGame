using fi.tamk.hellgame.character;
using fi.tamk.hellgame.utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.character
{

    public class DisplacingDamageCollider : MonoBehaviour
    {
        [SerializeField]
        private LayerMask HitLayer;

        protected void OnTriggerEnter(Collider hit)
        {
            if (HitLayer != (HitLayer | (1 << hit.gameObject.layer))) return;

            HealthComponent hc = Pool.Instance.GetHealthComponent(hit.gameObject);
            if (hc != null) hc.TakeDisplacingDamage(1);
        }
    }
}
