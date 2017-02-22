using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace fi.tamk.hellgame.character
{
    public delegate void PlayerCollectPointsEvent(int howManyPoints, int powerUpBreakPoint);

    public class PlayerLimitBreak : MonoBehaviour
    {
        public event PlayerCollectPointsEvent PowerUpGained;
        [SerializeField] private PlayerLimitBreakStats _originalStats;
        private PlayerLimitBreakStats _modifiableStats;

        public UnityEvent LimitBreakActivation;
        public UnityEvent LimitbreakEndEvent;
        private int _collectedPoints = 0;
        public bool LimitBreakActive { get; private set; }
        private HealthComponent _hc;

        void Start()
        {
            _modifiableStats = Instantiate(_originalStats);
            _hc = GetComponent<HealthComponent>();
            LimitBreakActive = false;
        }

        public void GainPoints(int howMany)
        {
            if (LimitBreakActive) return;

            _collectedPoints = Mathf.Clamp(_collectedPoints + howMany, 0, _modifiableStats.BreakPointLimit);
            if(PowerUpGained != null) PowerUpGained.Invoke(_collectedPoints, _modifiableStats.BreakPointLimit);
            if (_collectedPoints >= _modifiableStats.BreakPointLimit) LimitBreakActive = true;
        }

        public void ActivateLimitBreak()
        {
            if (_collectedPoints < _modifiableStats.BreakPointLimit) return;

            LimitBreakActivation.Invoke();
            _hc.ActivateInvulnerability(_modifiableStats.LimitBreakLenght);
            _collectedPoints = 0;
            StartCoroutine(LimitBreakTimer());
            _modifiableStats.BreakPointLimit += _modifiableStats.GetLatestBreakPointIncrease();
        }

        public void DeactivateLimitbreak()
        {
            LimitBreakActive = false;
            LimitbreakEndEvent.Invoke();
            PowerUpGained.Invoke(0, _modifiableStats.BreakPointLimit);
        }

        public void GetCurrentAmountAndThreshHold(out int currentAmount, out int currentThreshHold)
        {
            currentAmount = _collectedPoints;

            if (_modifiableStats == null)
            {
                _modifiableStats = Object.Instantiate(_originalStats) as PlayerLimitBreakStats;
            }

            currentThreshHold = _modifiableStats.BreakPointLimit;
        }

        private IEnumerator LimitBreakTimer()
        {
            var t = 0f;

            while (t <= 1)
            {
                t += Time.deltaTime / _modifiableStats.LimitBreakLenght;
                yield return null;
            }

            DeactivateLimitbreak();
        }
    }
}
