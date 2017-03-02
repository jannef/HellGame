using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fi.tamk.hellgame.effectors;
using fi.tamk.hellgame.utils;
using UnityEngine;

namespace fi.tamk.hellgame.character
{
    class HealthPickUp : MonoBehaviour
    {
        [SerializeField] private LayerMask _pickUpLayerMask;

        protected void OnTriggerEnter(Collider hit)
        {
            if (_pickUpLayerMask != (_pickUpLayerMask | (1 << hit.gameObject.layer))) return;

            var pc = ServiceLocator.Instance.GetPickupGatherer(hit.gameObject.transform);
            if (pc != null) pc.PickItem(PickupType.Health);
            

            var pickUpEffect = GetComponent<CollectiableDropEffect>();
            if (pickUpEffect == null) return;
            pickUpEffect.amountDropped = 0;
            var healthComponent = GetComponent<HealthComponent>();
            if (healthComponent != null) healthComponent.Die();
        }
    }
}
