using UnityEngine;
using System.Collections;
using fi.tamk.hellgame.utils;
using fi.tamk.hellgame.world;

namespace fi.tamk.hellgame.effects
{
    public class EmissionColorVariator : MonoBehaviour
    {
        [SerializeField] private Color _highEmission;
        [SerializeField] private Color _lowEmission;
        [SerializeField] private AnimationCurve _emissionCurve;
        [SerializeField] private float _emissionInterval;

        private Renderer _renderer;
        private float _timer = 0f;

        private static bool CalledOne = false;
        private void Awake()
        {
            _renderer = GetComponentInChildren<Renderer>();
        }

        private void Update()
        {
            CalledOne = true;
            _timer += WorldStateMachine.Instance.DeltaTime;

            if (_timer > _emissionInterval) _timer -= _emissionInterval;
            var ratio = _timer / _emissionInterval;

            _renderer.material.SetColor("_EmissionColor", Color.Lerp(_lowEmission, _highEmission, _emissionCurve.Evaluate(ratio)));
        }
    }
}
