using UnityEngine;
using System.Collections;
using fi.tamk.hellgame.world;

namespace fi.tamk.hellgame.effects
{
    public class SpehereShellPulse : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _curve;
        [SerializeField] private float _pulseDuration;
        [SerializeField] private float _pulseMagnitude;
        private float _timer = 0f;
        private Vector3 _originalScale;

        private void Awake()
        {
            _originalScale = transform.localScale;
        }

        private void Update()
        {
            _timer += WorldStateMachine.Instance.DeltaTime;
            if (_timer > _pulseDuration) _timer -= _pulseDuration;
            transform.localScale = _originalScale +
                                   Vector3.one * _pulseMagnitude * _curve.Evaluate(_timer / _pulseDuration);
        }
    }
}
