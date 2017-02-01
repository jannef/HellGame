using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fi.tamk.hellgame.utils.Stairs.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace fi.tamk.hellgame.character
{
    class SelfDestructComponent : MonoBehaviour
    {
        [SerializeField] protected int hitDamage;
        [SerializeField] protected UnityEvent OnHitEvent;
        [SerializeField] private LayerMask SelfDestructLayer;

        protected void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (SelfDestructLayer != (SelfDestructLayer | (1 << hit.gameObject.layer))) return;
            OnHitEvent.Invoke();
            HealthComponent hc = Pool.Instance.GetHealthComponent(hit.gameObject);
            GetComponent<HealthComponent>().TakeDamage(100);
            if (hc == null) return;
            hc.TakeDamage(hitDamage);
        }
    }
}
