using System.Reflection;
using System.Linq;
using UnityEngine;
using System;

namespace fi.tamk.hellgame.character
{
    public class LimitBreakParticles : MonoBehaviour
    {
        public float IndicatorParticlesPerSecond
        {
            set
            {
                _indicatorParticlesPerSecond = value;
                _emissionRateProperty.SetValue(_limitIndicator.emission, _indicatorActive || _aura ? 0f : value, null);
                if (_indicatorParticlesPerSecond < Mathf.Epsilon)
                {
                    var nbr = _limitIndicator.GetParticles(_indicatorBuffer);
                    for (var i = 0; i < nbr; ++i) _indicatorBuffer[i].remainingLifetime = -1f;
                    _limitIndicator.SetParticles(_indicatorBuffer, nbr);
                }

            }

            get { return _indicatorParticlesPerSecond; }
        }

        [SerializeField] private ParticleSystem _limitIndicator;
        [SerializeField] private ParticleSystem _limitAura;
        [SerializeField] private PlayerLimitBreak _limitController;
        [SerializeField] private float _indicatorInterval;
        [SerializeField] private int _indicatorAmounth;
        [SerializeField] private float _maxRadius;
        [SerializeField] private float _minRadius;
        [SerializeField] private AnimationCurve _radiusCurve;

        private float _originalAuraEmissionRate = 0f;
        private bool _indicatorActive = false;
        private bool _aura = false;
        private float _timer = 0f;
        private ParticleSystem.Particle[] _indicatorBuffer;
        private float _indicatorParticlesPerSecond;
        private PropertyInfo _emissionRateProperty;
        private PropertyInfo _emissionRadiusProperty;

        public void Awake()
        {
            _emissionRateProperty = typeof(ParticleSystem.EmissionModule).GetProperty("rateOverTimeMultiplier");
            // the radius property is called "radius" this is not within recommended naming convention for public property, so
            // this may change to "Radius" in further and thus need update here...
            _emissionRadiusProperty = typeof(ParticleSystem.ShapeModule).GetProperty("radius");

            _indicatorBuffer = new ParticleSystem.Particle[_limitIndicator.main.maxParticles];

            _originalAuraEmissionRate = _limitAura.emission.rateOverTimeMultiplier;
            _emissionRateProperty.SetValue(_limitAura.emission, 0f, null);

            _limitController.PowerUpGained += (x, y) => IndicatorParticlesPerSecond = x;
            _limitController.LimitBreakStateChange += SetAura;
            _limitController.LimitBreakDurationChange += SetEmissionRadius;
        }

        private void SetAura(bool aura, bool indicator)
        {
            
            _emissionRateProperty.SetValue(_limitAura.emission, aura ? _originalAuraEmissionRate : 0f, null);
            if (indicator)_emissionRadiusProperty.SetValue(_limitAura.shape, _minRadius, null);
            if (aura || indicator) IndicatorParticlesPerSecond = 0f;

            _indicatorActive = indicator;
            _aura = aura;
        }

        /// <summary>
        /// Sets distance of emission from center.
        /// </summary>
        /// <param name="percentage">Ratio between min and max value</param>
        private void SetEmissionRadius(float percentage)
        {
            _emissionRadiusProperty.SetValue(_limitAura.shape, Mathf.Lerp(_minRadius, _maxRadius, _radiusCurve.Evaluate(percentage)), null);
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            if (_indicatorActive && !_aura && _timer > _indicatorInterval)
            {
                _limitAura.Emit(_indicatorAmounth);
                _timer = 0f;
            }
        }
    }
}