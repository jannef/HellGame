using fi.tamk.hellgame.utils;
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
        [SerializeField] private float _wakeUpDelay;
        public UnityEvent ActivationEvent;
        public UnityEvent WakeUpEvent;
        private bool hasActivated = false;
        private BossDamageThresholdEffectMaster _master;

        private void Start()
        {
            _master = GetComponentInParent<BossDamageThresholdEffectMaster>();
            _master.DamageEvent += OnDamageTaken;
            _master.WakeUpEvent += BossWokeUp;
            HealthPercentage = 1f - ((1f / TotalAmountOfEffects) * OrderNumber);
        }

        private void BossWokeUp()
        {
            _master.WakeUpEvent -= BossWokeUp;
            StartCoroutine(StaticCoroutines.DoAfterDelay(_wakeUpDelay * (TotalAmountOfEffects - OrderNumber), WakeUpEvent.Invoke));
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
