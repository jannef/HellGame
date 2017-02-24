using fi.tamk.hellgame.physicsobjects;
using fi.tamk.hellgame.utils;
using System;
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
        private int _lastSeenHp; // We track this so we can deduce if the health change is due damage or healing...
        private HealthComponent _hc;
        private PlayerLimitBreakStats _modifiableStats;
        private bool _limitActive;

        protected void Awake()
        {
            _modifiableStats = ScriptableObject.Instantiate(_originalStats) as PlayerLimitBreakStats;
            _hc = GetComponent<HealthComponent>();
            LimitAvailableOrActive = false;
            _limitActive = false;
            _hc.HealthChangeEvent += OnPlayerHit;
            _lastSeenHp = _hc.Health;
        }

        private void OnPlayerHit(float percentage, int currentHp, int maxHp)
        {
            if (currentHp < _lastSeenHp) {       
                for (var i = 0; i < _collectedPoints; i++)
                {
                    var go = Pool.Instance.GetObject(Pool.PickupPrefab);
                    go.transform.position = transform.position + Vector3.up * 3f + UnityEngine.Random.onUnitSphere * 3f;
                    go.GetComponent<Rigidbody>().AddExplosionForce(60f, transform.position + Vector3.up * 4f, 5f);
                    go.GetComponent<Pickup>().DisablePickupTemporarily(1f);
                }                
                GainPoints(-_collectedPoints);
            }
            _lastSeenHp = currentHp;
        }

        public void GainPoints(int howMany)
        {
            if (LimitAvailableOrActive && howMany > 0) return;

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
            PowerUpGained.Invoke(0, _modifiableStats.BreakPointLimit);
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
