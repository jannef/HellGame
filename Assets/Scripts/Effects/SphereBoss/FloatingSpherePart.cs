using System.Collections;
using System.Collections.Generic;
using fi.tamk.hellgame.world;
using UnityEngine;

namespace fi.tamk.hellgame.effects
{
    public class FloatingSpherePart : MonoBehaviour
    {
        [SerializeField] private Vector3 _normal;
        public float RotationSpeed = 10f;

        private Coroutine _rotationTween;
        private Coroutine _hoverTween;

        private Collider _collider;
        public Rigidbody Rigidbody;

        private bool _isDestroyed = false;
        private float _vanishTimer = 0f;
        private Coroutine _vanishCoroutine;

        public void Init(float initialRotation)
        {
            RotationSpeed = initialRotation;
            _collider = GetComponent<Collider>();
            Rigidbody = GetComponent<Rigidbody>();
        }

        public void ReleaseFromParent()
        {
            transform.parent = null;
            _collider.enabled = true;
            Rigidbody.isKinematic = false;
            StopAllCoroutines();
            _isDestroyed = true;
        }

        private void Update()
        {
            if (!_isDestroyed)
            {
                RotateAroundNormal();
            }
            else if (_vanishCoroutine == null)
            {
                _vanishTimer += WorldStateMachine.Instance.DeltaTime;
                if (_vanishTimer > 10f)
                {
                    _vanishCoroutine = StartCoroutine(VanishAfterDestroyed(5f, Vector3.down * 0.66f));
                }
            }
        }

        private void RotateAroundNormal()
        {
            transform.Rotate(_normal, RotationSpeed * WorldStateMachine.Instance.DeltaTime, Space.Self);
        }

        public void ChangeRotationSpeedOverTime(float newRotation, float transitionDuration)
        {
            if (_rotationTween != null) StopCoroutine(_rotationTween);
            _rotationTween = StartCoroutine(TweenRotationSpeed(newRotation, transitionDuration));
        }

        public void HoverAwayFromCore(float height, float duration, AnimationCurve curve)
        {
            if (_hoverTween == null)
            {
                _hoverTween = StartCoroutine(HoverInNormalDirection(height, duration, curve));
            }
        }

        private IEnumerator VanishAfterDestroyed(float duration, Vector3 speed)
        {
            Rigidbody.isKinematic = true;
            _collider.enabled = false;

            var timer = 0f;
            while (timer < duration)
            {
                timer += WorldStateMachine.Instance.DeltaTime;
                transform.position += speed * WorldStateMachine.Instance.DeltaTime;
                yield return null;
            }

            Destroy(gameObject);
        }

        private Vector3 _oldPosition;
        private IEnumerator HoverInNormalDirection(float height, float duration, AnimationCurve curve)
        {
            if (!(duration >= 0.1f)) yield break;

            var timer = 0f;
            _oldPosition = transform.localPosition;
            var newPosition = _oldPosition + _normal * height;
            while (timer < duration)
            {
                timer += WorldStateMachine.Instance.DeltaTime;
                transform.localPosition = Vector3.Lerp(_oldPosition, newPosition, curve.Evaluate(timer / duration));
                yield return null;
            }

            transform.localPosition = _oldPosition; // Just to be sure
            _hoverTween = null;
        }

        private IEnumerator TweenRotationSpeed(float newSpeed, float duration)
        {
            if (!(duration >= 0.1f)) yield break;

            var timer = 0f;
            var oldSpeed = RotationSpeed;
            while (timer < duration)
            {
                timer += WorldStateMachine.Instance.DeltaTime;
                RotationSpeed = Mathf.Lerp(oldSpeed, newSpeed, timer / duration);
                yield return null;
            }
            _rotationTween = null;
        }
    }
}
