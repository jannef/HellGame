using UnityEngine;
using System.Collections;
using fi.tamk.hellgame.world;

namespace fi.tamk.hellgame.effects
{
    /// <summary>
    /// Variator for light source intensity.
    /// </summary>
    public class LightIntensityVariator : MonoBehaviour
    {
        /// <summary>
        /// Pulsing curve for a the light intensity cycle.
        /// </summary>
        [SerializeField] private AnimationCurve _curve;

        /// <summary>
        /// Cycöe duration.
        /// </summary>
        [SerializeField] private float _cycleDuration;

        /// <summary>
        /// Magnitude delta that corresponds with 1.0f in the curve.
        /// </summary>
        [SerializeField] private float _cycleMagnitude;

        /// <summary>
        /// Reference to the light.
        /// </summary>
        private Light _light;

        /// <summary>
        /// Internal timer.
        /// </summary>
        private float _timer = 0f;

        /// <summary>
        /// Original intensity value at startup.
        /// </summary>
        private float _originalIntensity;

        /// <summary>
        /// Caches references, randomizes timer phase and stores original intensity.
        /// </summary>
        private void Awake()
        {
            _light = GetComponent<Light>();
            _originalIntensity = _light.intensity;
            _timer = Random.Range(0, _cycleDuration);
        }

        /// <summary>
        /// Update cycle, evaluates given curve in a loop.
        /// </summary>
        private void Update()
        {
            _timer += WorldStateMachine.Instance.DeltaTime;
            if (_timer > _cycleDuration) _timer -= _cycleDuration;
            _light.intensity = _originalIntensity + _curve.Evaluate(_timer / _cycleDuration) * _cycleMagnitude;
        }
    }
}
