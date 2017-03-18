using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.character
{

    public class HealthBlinkComponent : MonoBehaviour
    {
        [SerializeField] private Renderer _renderer;
        [SerializeField] private float _startFrequency;
        [SerializeField] private float _endFrequency;
        [SerializeField] private Color _colorToBlinkInto;
        [SerializeField] private float _damageThreshold = 0.5f;
        [SerializeField] private AnimationCurve _frequencyEasing;
        private float _currentFrequency;
        private Color _startColor;

        void Awake()
        {
            var hc = GetComponent<HealthComponent>();
            hc.HealthChangeEvent += HealthChanged;
            this.enabled = false;
            _startColor = _renderer.material.color;
        }

        private void HealthChanged(float percentage, int current, int max)
        {
            if (percentage <= _damageThreshold && percentage > 0)
            {
                enabled = true;
                _currentFrequency = Mathf.Lerp(_endFrequency, _startFrequency, _frequencyEasing.Evaluate(percentage * 2));
            }
        }

        void Update()
        {
            if (!_renderer.enabled) return;

            float lerp = Mathf.Sin(Time.time * _currentFrequency);
            _renderer.material.color = Color.Lerp(_startColor, _colorToBlinkInto, lerp);
        }
    }
}
