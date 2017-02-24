using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace fi.tamk.hellgame.character
{
    public delegate void PlayerCollectPointsEvent(int howManyPoints, int powerUpBreakPoint);

    public class PlayerLimitBreak : MonoBehaviour
    {
        public event UnityAction<bool, bool> LimitBreakStateChange; 
        public event PlayerCollectPointsEvent PowerUpGained;
        public bool LimitAvailableOrActive { get; private set; }

        public UnityEvent LimitBreakActivation;
        public UnityEvent LimitbreakEndEvent;        

        [SerializeField] private PlayerLimitBreakStats _originalStats;

        private int _collectedPoints = 0;
        private HealthComponent _hc;
        private PlayerLimitBreakStats _modifiableStats;
        private bool _limitActive;

        protected void Awake()
        {
            _modifiableStats = ScriptableObject.Instantiate(_originalStats) as PlayerLimitBreakStats;
            _hc = GetComponent<HealthComponent>();
            LimitAvailableOrActive = false;
            _limitActive = false;
        }

        public void GainPoints(int howMany)
        {
            if (LimitAvailableOrActive) return;

            _collectedPoints = Mathf.Clamp(_collectedPoints + howMany, 0, _modifiableStats.BreakPointLimit);
            if(PowerUpGained != null) PowerUpGained.Invoke(_collectedPoints, _modifiableStats.BreakPointLimit);
            LimitAvailableOrActive = _collectedPoints >= _modifiableStats.BreakPointLimit;
            if (LimitBreakStateChange != null) LimitBreakStateChange.Invoke(_limitActive, LimitAvailableOrActive);
        }

        public void ActivateLimitBreak()
        {
            if (!LimitAvailableOrActive) return;

            _limitActive = true;            
            LimitBreakActivation.Invoke();
            if (PowerUpGained != null) PowerUpGained.Invoke(0, _modifiableStats.BreakPointLimit);
            _hc.ActivateInvulnerability(_modifiableStats.LimitBreakLenght);
            _collectedPoints = 0;
            StartCoroutine(LimitBreakTimer());
            _modifiableStats.BreakPointLimit += _modifiableStats.GetLatestBreakPointIncrease();
            if (LimitBreakStateChange != null) LimitBreakStateChange.Invoke(_limitActive, LimitAvailableOrActive);
        }

        protected void DeactivateLimitbreak()
        {
            LimitAvailableOrActive = false;
            LimitbreakEndEvent.Invoke();
            if (PowerUpGained != null) PowerUpGained.Invoke(0, _modifiableStats.BreakPointLimit);
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
            _limitActive = false;
            if (LimitBreakStateChange != null) LimitBreakStateChange.Invoke(_limitActive, LimitAvailableOrActive);
        }
    }
}
