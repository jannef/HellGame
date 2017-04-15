using UnityEngine;
using System.Collections;
using fi.tamk.hellgame.utils;
using fi.tamk.hellgame.world;

namespace fi.tamk.hellgame.effects
{
    public class EmissionColorVariator : MonoBehaviour
    {
        [SerializeField] private Material _material;
        [SerializeField] private Color _highEmission;
        [SerializeField] private Color _lowEmission;
        [SerializeField] private AnimationCurve _emissionCurve;
        [SerializeField] private float _emissionInterval;

        private float _timer = 0f;

        private Color _startColor;

#if UNITY_EDITOR
        private void Awake()
        {
            _startColor = _material.GetColor("_EmissionColor");
        }
#endif
        private void Update()
        {
            _timer += WorldStateMachine.Instance.DeltaTime;

            if (_timer > _emissionInterval) _timer -= _emissionInterval;
            var ratio = _timer / _emissionInterval;

            _material.SetColor("_EmissionColor", Color.Lerp(_lowEmission, _highEmission, _emissionCurve.Evaluate(ratio)));
        }

#if UNITY_EDITOR
        private void OnDestroy()
        {
            _material.SetColor("_EmissionColor", _startColor);
        }
#endif
    }
}
