using UnityEngine;
using System.Collections;
using fi.tamk.hellgame.world;

namespace fi.tamk.hellgame.effects
{
    public class LightIntensityVariator : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _curve;
        [SerializeField] private float _cycleDuration;
        [SerializeField] private float _cycleMagnitude;
        [SerializeField] private bool RandomCycleStart = true;
        private Light _light;

        private float _timer = 0f;
        private float _originalIntensity;

        private void Awake()
        {
            _light = GetComponent<Light>();
            _originalIntensity = _light.intensity;
            _timer = Random.Range(0, _cycleDuration);
        }

        private void Update()
        {
            _timer += WorldStateMachine.Instance.DeltaTime;
            if (_timer > _cycleDuration) _timer -= _cycleDuration;
            _light.intensity = _originalIntensity + _curve.Evaluate(_timer / _cycleDuration) * _cycleMagnitude;
        }
    }
}
