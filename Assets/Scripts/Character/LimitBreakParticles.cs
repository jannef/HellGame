using System.Reflection;
using System.Linq;
using UnityEngine;
using System;

namespace fi.tamk.hellgame.character
{
    public class LimitBreakParticles : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _limitIndicator;
        [SerializeField] private ParticleSystem _limitAura;
        [SerializeField] private PlayerLimitBreak _limitController;
        [SerializeField] private float _indicatorInterval;
        [SerializeField] private int _indicatorAmounth;

        private float _originalAuraEmissionRate = 0f;
        private bool _indicatorActive = false;
        private bool _aura = false;
        private float _timer = 0f;

        public float ParticlesPerSecond
        {
            set
            {
                _particlesPerSecond = value;
                _emissionRateProperty.SetValue(_limitIndicator.emission, value, null);
            }

            get { return _particlesPerSecond; }
        }
        private float _particlesPerSecond;
        private PropertyInfo _emissionRateProperty;

        public void Awake()
        {
            _emissionRateProperty = typeof(ParticleSystem.EmissionModule).GetProperty("rateOverTimeMultiplier");

            _originalAuraEmissionRate = _limitAura.emission.rateOverTimeMultiplier;
            _emissionRateProperty.SetValue(_limitAura.emission, 0f, null);

            _limitController.PowerUpGained += (x, y) => ParticlesPerSecond = x;
            _limitController.LimitBreakStateChange += SetAura;
        }

        private void SetAura(bool aura, bool indicator)
        {
            _indicatorActive = indicator;
            _aura = aura;
            _emissionRateProperty.SetValue(_limitAura.emission, aura ? _originalAuraEmissionRate : 0f, null);
            if (aura || indicator) ParticlesPerSecond = 0;
        }

        private void Update()
        {
            Debug.Log(_aura + " " + _indicatorActive);
            _timer += Time.deltaTime;
            if (_indicatorActive && !_aura && _timer > _indicatorInterval)
            {
                _limitAura.Emit(_indicatorAmounth);
                _timer = 0f;
            }
        }
    }
}