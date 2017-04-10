using fi.tamk.hellgame.utils;
using UnityEngine;
using UnityEngine.Events;

namespace fi.tamk.hellgame.character
{
    class SelfDestructComponent : MonoBehaviour
    {
        [SerializeField] protected int hitDamage;
        [SerializeField] protected UnityEvent OnHitEvent;
        [SerializeField] private LayerMask SelfDestructLayer;

        protected void OnTriggerEnter(Collider hit)
        {
            if (SelfDestructLayer != (SelfDestructLayer | (1 << hit.gameObject.layer))) return;
            if (OnHitEvent != null) OnHitEvent.Invoke();

            HealthComponent hc = Pool.Instance.GetHealthComponent(hit.gameObject);
            if (hc != null) hc.TakeDamage(hitDamage);
        }
    }
}
