﻿using fi.tamk.hellgame.physicsobjects;
using fi.tamk.hellgame.utils;
using System;
using System.Collections;
using fi.tamk.hellgame.world;
using UnityEngine;
using UnityEngine.Events;
using fi.tamk.hellgame.effects;

namespace fi.tamk.hellgame.character
{
    public class PlayerLimitBreak : MonoBehaviour
    {
        /// <summary>
        /// First parameter flag for limit break up.
        /// Second parameter flag for limit break charging.
        /// </summary>
        public event UnityAction<bool, bool> LimitBreakStateChange;

        /// <summary>
        /// First parameter is how many motes of power is collected.
        /// Second parameter is how many motes is needed to activate the limit break.
        /// </summary>
        public event UnityAction<float, int> PowerUpGained;

        /// <summary>
        /// Gives out duration in range of [0, 1], 0 being start of the limit break, 1 being over.
        /// </summary>
        public event UnityAction<float> LimitBreakDurationChange;

        /// <summary>
        /// Flag for limit break being active or activatable.
        /// </summary>
        public bool LimitAvailableOrActive { get; private set; }

        /// <summary>
        /// Invoked at the start of the limit break.
        /// </summary>
        public UnityEvent LimitBreakActivation;

        /// <summary>
        /// Invoked at the end of the limit break.
        /// </summary>
        public UnityEvent LimitbreakEndEvent;

        public UnityEvent DudActivation;

        /// <summary>
        /// Original stats of the player.
        /// </summary>
        [SerializeField] private PlayerLimitBreakStats _originalStats;

        [SerializeField] private float DudEffectCoolDownLenght;

        /// <summary>
        /// Original stats of the player.
        /// </summary>
        [SerializeField]
        private float _onExplosionCollectiableDropAmountMultiplier;

        /// <summary>
        /// Public getter for _collected points;
        /// </summary>
        public float CollectedPoints { get { return _collectedPoints; } }

        /// <summary>
        /// How many motes of power is currently collected.
        /// </summary>
        private float _collectedPoints;

        private bool _dudEffectAvailable = true;

        /// <summary>
        /// What is the last seen number of hitpoints from this actors HealthComponent.
        /// This is stored to ignore healing causing loss of collected power, if such
        /// mechanic is ever introduced.
        /// </summary>
        private int _lastSeenHp; // We track this so we can deduce if the health change is due damage or healing...

        /// <summary>
        /// Reference to associated HealthComponent.
        /// </summary>
        private HealthComponent _hc;

        /// <summary>
        /// Instansiated copy of the stats used by this limit break.
        /// </summary>
        private PlayerLimitBreakStats _modifiableStats;

        /// <summary>
        /// If this limit break is active.
        /// </summary>
        public bool LimitActive;

        [SerializeField] private PlayerCollectiableSoundEffect _collectionSoundEffect;

        /// <summary>
        /// Initializes references and registers for health change event.
        /// </summary>
        protected void Awake()
        {
            _modifiableStats = ScriptableObject.Instantiate(_originalStats) as PlayerLimitBreakStats;
            _hc = GetComponent<HealthComponent>();
            LimitAvailableOrActive = false;
            LimitActive = false;
            _hc.HealthChangeEvent += OnPlayerHit;
            _lastSeenHp = _hc.Health;
        }

        /// <summary>
        /// If player is hit, releasess the currently collected power motes in a sonic
        /// style explosion.
        /// </summary>
        /// <param name="percentage">Not used.</param>
        /// <param name="currentHp">compared to value last seen by this to determine if the health change is due damage, and not healing.</param>
        /// <param name="maxHp">Not used.</param>
        private void OnPlayerHit(float percentage, int currentHp, int maxHp)
        {
            if (currentHp < _lastSeenHp) {       
                for (var i = 0; i < _collectedPoints * _onExplosionCollectiableDropAmountMultiplier; i++)
                {
                    var go = Pool.Instance.GetObject(Pool.PickupPrefab);
                    go.transform.position = transform.position + Vector3.up * 3f + UnityEngine.Random.onUnitSphere * 3f;
                    go.GetComponent<Rigidbody>().AddExplosionForce(250f, transform.position + Vector3.up * 4f, 5f);
                    go.GetComponent<Pickup>().DisablePickupTemporarily(1f);
                }                
                GainPoints(-_collectedPoints);
            }
            _lastSeenHp = currentHp;
        }

        /// <summary>
        /// Adds to collected power motes number.
        /// </summary>
        /// <param name="howMany">How many to add.</param>
        public void GainPoints(float howMany)
        {
            if (_collectionSoundEffect != null) _collectionSoundEffect.GemCollected();
            if ((LimitAvailableOrActive && !LimitActive) && howMany > 0) return;


            _collectedPoints = Mathf.Clamp(_collectedPoints + howMany, 0, _modifiableStats.Cost);


            if(PowerUpGained != null) PowerUpGained.Invoke(_collectedPoints, _modifiableStats.Cost);
            LimitAvailableOrActive = _collectedPoints >= _modifiableStats.Cost;

            if (LimitBreakStateChange != null) LimitBreakStateChange.Invoke(LimitActive, LimitAvailableOrActive);
        }

        /// <summary>
        /// Activates the limit break.
        /// </summary>
        public void ActivateLimitBreak()
        {
            if (!LimitAvailableOrActive)
            {
                if (_dudEffectAvailable) DudLimitBreakEffect();
                return;
            }

            LimitActive = true;            
            LimitBreakActivation.Invoke();
            if (PowerUpGained != null) PowerUpGained.Invoke(0, _modifiableStats.Cost); // Indicator is hooked into this.
            _hc.ActivateInvulnerability(_modifiableStats.DesiredBaseLenght);            
            StartCoroutine(LimitBreakTimer());
            if (LimitBreakStateChange != null) LimitBreakStateChange.Invoke(LimitActive, LimitAvailableOrActive); // Indicator will change state. 
        }

        private void DudLimitBreakEffect()
        {
            if (DudActivation != null) DudActivation.Invoke();
            _dudEffectAvailable = false;
            StartCoroutine(StaticCoroutines.DoAfterDelay(DudEffectCoolDownLenght, StopDudEffectCooldown));
        }

        private void StopDudEffectCooldown()
        {
            _dudEffectAvailable = true;
        }

        /// <summary>
        /// Deactivates the limit break.
        /// </summary>
        protected void DeactivateLimitbreak()
        {
            _collectedPoints = 0;
            LimitAvailableOrActive = false;
            LimitbreakEndEvent.Invoke();
            if (PowerUpGained != null) PowerUpGained.Invoke(0, _modifiableStats.Cost);
        }

        /// <summary>
        /// Counts down until end of the limit break;
        /// </summary>
        /// <returns></returns>
        private IEnumerator LimitBreakTimer()
        {
            var pps = _modifiableStats.Cost / _modifiableStats.DesiredBaseLenght;

            while (_collectedPoints > 0)
            {
                if (LimitBreakDurationChange != null) LimitBreakDurationChange.Invoke(_collectedPoints / _modifiableStats.Cost);
                pps *= 1f + (_modifiableStats.CostMultiplierPerSecond - 1f) * WorldStateMachine.Instance.DeltaTime;
                _collectedPoints -= pps * WorldStateMachine.Instance.DeltaTime;
                yield return null;
            }

            DeactivateLimitbreak();
            LimitActive = false;
            if (LimitBreakStateChange != null) LimitBreakStateChange.Invoke(LimitActive, LimitAvailableOrActive);
        }
    }
}
