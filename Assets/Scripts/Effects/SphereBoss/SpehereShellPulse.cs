using UnityEngine;
using System.Collections;
using fi.tamk.hellgame.world;

namespace fi.tamk.hellgame.effects
{
    /// <summary>
    /// Pulsing effect on sphere boss.
    /// </summary>
    public class SpehereShellPulse : MonoBehaviour
    {
        /// <summary>
        /// Radius pulse curve..
        /// </summary>
        [SerializeField] private AnimationCurve _curve;

        /// <summary>
        /// Loop time for the given curve.
        /// </summary>
        [SerializeField] private float _pulseDuration;

        /// <summary>
        /// Magnitude that 1.0f at curve corresponds to.
        /// </summary>
        [SerializeField] private float _pulseMagnitude;

        /// <summary>
        /// Internal timer.
        /// </summary>
        private float _timer = 0f;

        /// <summary>
        /// Original scale of the object.
        /// </summary>
        private Vector3 _originalScale;

        /// <summary>
        /// Caches original scale of the object.
        /// </summary>
        private void Awake()
        {
            _originalScale = transform.localScale;
        }

        /// <summary>
        /// Iterates the curve on loop and scales the object accordingly.
        /// </summary>
        private void Update()
        {
            _timer += WorldStateMachine.Instance.DeltaTime;
            if (_timer > _pulseDuration) _timer -= _pulseDuration;
            transform.localScale = _originalScale +
                                   Vector3.one * _pulseMagnitude * _curve.Evaluate(_timer / _pulseDuration);
        }
    }
}
