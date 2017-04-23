using fi.tamk.hellgame.character;
using fi.tamk.hellgame.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

namespace fi.tamk.hellgame.character
{
    class DamagingCollider : MonoBehaviour
    {
        public UnityEvent TriggerEnter;
        public UnityEvent TriggerDamage;

        [SerializeField] private LayerMask DamageLayer;
        [SerializeField] private int damage;

        private void OnTriggerStay(Collider other)
        {
            if (DamageLayer != (DamageLayer | (1 << other.gameObject.layer))) return;

            if (TriggerDamage != null) TriggerDamage.Invoke();
            HealthComponent hc = Pool.Instance.GetHealthComponent(other.gameObject);
            if (hc != null) hc.TakeDamage(damage);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (DamageLayer != (DamageLayer | (1 << other.gameObject.layer))) return;
            if (TriggerEnter != null) TriggerEnter.Invoke();
        }
    }
}
