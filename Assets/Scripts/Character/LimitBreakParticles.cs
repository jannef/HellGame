#define USE_CS

using System.Reflection;
using System.Linq;
using UnityEngine;
using System;
using fi.tamk.hellgame.world;
using fi.tamk.hellgame.effects;
using UnityEngine.Events;

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
        [SerializeField, Range(0.1f, 10f)] float _indicatorParticleAmountMultiplier;
        [SerializeField] private float _maxRadius;
        [SerializeField] private float _minRadius;
        [SerializeField] private AnimationCurve _radiusCurve;
        public UnityEvent LimitBreakAvailable;

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

            _limitController.PowerUpGained += (x, y) => IndicatorParticlesPerSecond = x * _indicatorParticleAmountMultiplier;
            _limitController.LimitBreakStateChange += SetAura;
            _limitController.LimitBreakDurationChange += SetEmissionRadius;

// Initialization for compute shaders.
#if USE_CS
            _auraBuffer = new ParticleSystem.Particle[_limitAura.main.maxParticles];
            _particlePositions = new BufferDataType[_limitAura.main.maxParticles];
            _computeBuffer = new ComputeBuffer(_limitAura.main.maxParticles, 16); // second parameter is bytecount to be held in the buffer
#endif
        }

        private void SetAura(bool aura, bool indicator)
        {
            
            if (indicator && !aura)
            {
                if (LimitBreakAvailable != null) LimitBreakAvailable.Invoke();
            }

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
            _timer += WorldStateMachine.Instance.DeltaTime;
            if (_indicatorActive && !_aura && _timer > _indicatorInterval)
            {
                _limitAura.Emit(_indicatorAmounth);
                _timer = 0f;
            }
#if USE_CS
            if (_aura && _cs != null)
            {
                ControlAura();
            }
#endif
        }

        [SerializeField] private ComputeShader _cs; // Unity can't serialize fields inside custom precompiler conditional statement.
// Compute shader block
#if USE_CS
        private struct BufferDataType
        {
            public Vector3 position;
            public float time;
        }

        private ParticleSystem.Particle[] _auraBuffer;
        private BufferDataType[] _particlePositions;
        private ComputeBuffer _computeBuffer;

        private void ControlAura()
        {
            var numberOfParticles = _limitAura.GetParticles(_auraBuffer);
            if (numberOfParticles < 1) return;

            for (var i = 0; i < numberOfParticles; ++i)
            {
                _particlePositions[i].position = _auraBuffer[i].position;
                _particlePositions[i].time = _auraBuffer[i].remainingLifetime;
            }

            _computeBuffer.SetData(_particlePositions);
            var kernel = _cs.FindKernel("CSMain");

            _cs.SetBuffer(kernel, "dataBuffer", _computeBuffer);
            _cs.SetFloat("maxTime", _auraBuffer[0].startLifetime); // We have tested that the buffer has atleast one particle in it at the start of the method.
            _cs.SetFloat("level", _limitAura.gameObject.transform.position.y);

            _cs.Dispatch(kernel, numberOfParticles, 1, 1);
            _computeBuffer.GetData(_particlePositions);

            for (var i = 0; i < numberOfParticles; ++i)
            {
                _auraBuffer[i].position = _particlePositions[i].position;
            }

            _limitAura.SetParticles(_auraBuffer, numberOfParticles);
        }
#endif

        private void OnDestroy()
        {
            _computeBuffer.Dispose();
        }
    }
}