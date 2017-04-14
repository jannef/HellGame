using fi.tamk.hellgame.character;
using fi.tamk.hellgame.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace fi.tamk.hellgame.character
{
    class DamagingCollider : MonoBehaviour
    {
        [SerializeField] private LayerMask DamageLayer;
        [SerializeField] private int damage;

        private void OnTriggerStay(Collider other)
        {
            if (DamageLayer != (DamageLayer | (1 << other.gameObject.layer))) return;

            HealthComponent hc = Pool.Instance.GetHealthComponent(other.gameObject);
            if (hc != null) hc.TakeDamage(damage);
        }
    }
}
