using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VR.WSA.Persistence;
using Object = UnityEngine.Object;

namespace fi.tamk.hellgame.character
{
    public delegate void PlayerCollectPointsEvent(int howManyPoints, int powerUpBreakPoint);

    public class PlayerLimitBreak : MonoBehaviour
    {
        public event PlayerCollectPointsEvent powerUpGained;
        [SerializeField] private PlayerLimitBreakStats originalStats;
        private PlayerLimitBreakStats modifiableStats;

        public UnityEvent limitBreakActivation;
        public UnityEvent limitbreakEndEvent;
        private int _collectedPoints = 0;
        public bool LimitBreakActive { get; private set; }
        private HealthComponent _hc;

        void Start()
        {
            modifiableStats = Object.Instantiate(originalStats) as PlayerLimitBreakStats;
            _hc = GetComponent<HealthComponent>();
            LimitBreakActive = false;
        }

        public void GainPoints(int howMany)
        {
            if (LimitBreakActive) return;

            _collectedPoints = Mathf.Clamp(_collectedPoints + howMany, 0, modifiableStats.BreakPointLimit);
            powerUpGained.Invoke(_collectedPoints, modifiableStats.BreakPointLimit);
            if (_collectedPoints >= modifiableStats.BreakPointLimit) LimitBreakActive = true;
        }

        public void ActivateLimitBreak()
        {
            if (_collectedPoints < modifiableStats.BreakPointLimit) return;

            limitBreakActivation.Invoke();
            _hc.ActivateInvulnerability(modifiableStats.LimitBreakLenght);
            _collectedPoints = 0;
            StartCoroutine(LimitBreakTimer());
            modifiableStats.BreakPointLimit += modifiableStats.GetLatestBreakPointIncrease();
        }

        public void DeactivateLimitbreak()
        {
            LimitBreakActive = false;
            limitbreakEndEvent.Invoke();
            powerUpGained.Invoke(0, modifiableStats.BreakPointLimit);
        }

        public void GetCurrentAmountAndThreshHold(out int currentAmount, out int currentThreshHold)
        {
            currentAmount = _collectedPoints;

            if (modifiableStats == null)
            {
                modifiableStats = Object.Instantiate(originalStats) as PlayerLimitBreakStats;
            }

            currentThreshHold = modifiableStats.BreakPointLimit;
        }

        private IEnumerator LimitBreakTimer()
        {
            var t = 0f;

            while (t <= 1)
            {
                t += Time.deltaTime / modifiableStats.LimitBreakLenght;
                yield return null;
            }

            DeactivateLimitbreak();
        }
    }
}
