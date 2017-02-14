using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VR.WSA.Persistence;

namespace fi.tamk.hellgame.character
{
    public delegate void PlayerCollectPointsEvent(int howManyPoints, int powerUpBreakPoint);

    public class PlayerLimitBreak : MonoBehaviour
    {
        public event PlayerCollectPointsEvent powerUpGained;
        [SerializeField] private PlayerLimitBreakStats stats;

        public UnityEvent limitBreakActivation;
        public UnityEvent limitbreakEndEvent;
        private int _collectedPoints = 0;
        public bool LimitBreakActive { get; private set; }
        private HealthComponent _hc;

        void Start()
        {
            _hc = GetComponent<HealthComponent>();
            LimitBreakActive = false;
        }

        public void GainPoints(int howMany)
        {
            if (LimitBreakActive) return;

            _collectedPoints = Mathf.Clamp(_collectedPoints + howMany, 0, stats.BreakPointLimit);
            powerUpGained.Invoke(_collectedPoints, stats.BreakPointLimit);
            if (_collectedPoints >= stats.BreakPointLimit) LimitBreakActive = true;
        }

        public void ActivateLimitBreak()
        {
            if (_collectedPoints < stats.BreakPointLimit) return;

            limitBreakActivation.Invoke();
            _hc.ActivateInvulnerability(stats.LimitBreakLenght);
            _collectedPoints = 0;
            StartCoroutine(LimitBreakTimer());
        }

        public void DeactivateLimitbreak()
        {
            LimitBreakActive = false;
            limitbreakEndEvent.Invoke();
            powerUpGained.Invoke(0, stats.BreakPointLimit);
        }

        private IEnumerator LimitBreakTimer()
        {
            var t = 0f;

            while (t <= 1)
            {
                t += Time.deltaTime / stats.LimitBreakLenght;
                yield return null;
            }

            DeactivateLimitbreak();


        }
    }
}
