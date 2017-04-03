using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace fi.tamk.hellgame.world
{
    public class BossDamageThresholdEffect : MonoBehaviour
    {
        private float HealthPercentage;
        [SerializeField] private float TotalAmountOfEffects;
        [SerializeField] private float OrderNumber;
        public UnityEvent ActivationEvent;
        private bool hasActivated = false;
        private BossDamageThresholdEffectMaster _master;

        private void Start()
        {
            _master = GetComponentInParent<BossDamageThresholdEffectMaster>();
            _master.DamageEvent += OnDamageTaken;
            HealthPercentage = 1f - ((1f / TotalAmountOfEffects) * OrderNumber);
        }

        private void OnDamageTaken(float percentage)
        {
            if (!hasActivated && percentage <= HealthPercentage)
            {
                hasActivated = true;
                if (ActivationEvent != null)
                {
                    ActivationEvent.Invoke();
                }

                if (_master != null) _master.DamageEvent -= OnDamageTaken;
            }
        }
    }
}
