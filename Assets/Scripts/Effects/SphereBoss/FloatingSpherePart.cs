using System.Collections;
using System.Collections.Generic;
using fi.tamk.hellgame.world;
using UnityEngine;

namespace fi.tamk.hellgame.effects
{
    /// <summary>
    /// Part of a shell on sphere boss.
    /// </summary>
    public class FloatingSpherePart : MonoBehaviour
    {
        /// <summary>
        /// Normal direction of this shell part. Must be configured manually since all the shell parts
        /// have same pivot location at origo of the boss.
        /// </summary>
        [SerializeField] private Vector3 _normal;

        /// <summary>
        /// Rotation speed of the shell part along the normal.
        /// </summary>
        public float RotationSpeed = 10f;

        /// <summary>
        /// Reference to rotation coroutine being ran.
        /// </summary>
        private Coroutine _rotationTween;

        /// <summary>
        /// Reference to hovering coroutine being ran.
        /// </summary>
        private Coroutine _hoverTween;

        /// <summary>
        /// Reference to convex collider on this shell piece.
        /// </summary>
        private Collider _collider;

        /// <summary>
        /// Reference to rigidbody on this shell piece.
        /// </summary>
        public Rigidbody Rigidbody;

        /// <summary>
        /// If this shell piece is blown off the boss.
        /// </summary>
        private bool _isDestroyed = false;

        /// <summary>
        /// Timer to vanish into floor.
        /// </summary>
        private float _vanishTimer = 0f;

        /// <summary>
        /// Vanishing coroutine running.
        /// </summary>
        private Coroutine _vanishCoroutine;

        /// <summary>
        /// Initializes script by setting references.
        /// </summary>
        /// <remarks>
        /// Called from SpehereShellController.
        /// </remarks>
        /// <param name="initialRotation">Initial rotation speed to set.</param>
        public void Init(float initialRotation = 0f)
        {
            RotationSpeed = initialRotation;
            _collider = GetComponent<Collider>();
            Rigidbody = GetComponent<Rigidbody>();
        }

        /// <summary>
        /// SphereShellController calls this when this needs to be realeased from it's parent.
        /// </summary>
        public void ReleaseFromParent()
        {
            transform.parent = null;
            _collider.enabled = true;
            Rigidbody.isKinematic = false;
            StopAllCoroutines();
            _isDestroyed = true;
        }

        /// <summary>
        /// Starts vanish coroutine.
        /// </summary>
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

        /// <summary>
        /// Rotates around _normal direction configured in Unity editor.
        /// </summary>
        private void RotateAroundNormal()
        {
            transform.Rotate(_normal, RotationSpeed * WorldStateMachine.Instance.DeltaTime, Space.Self);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newRotation"></param>
        /// <param name="transitionDuration"></param>
        public void ChangeRotationSpeedOverTime(float newRotation, float transitionDuration)
        {
            if (_rotationTween != null) StopCoroutine(_rotationTween);
            _rotationTween = StartCoroutine(TweenRotationSpeed(newRotation, transitionDuration));
        }

        /// <summary>
        /// Starts hovering coroutine.
        /// </summary>
        /// <param name="height">height to hover to</param>
        /// <param name="duration">How long from start to end should it take</param>
        /// <param name="curve">Curve along which to animate</param>
        public void HoverAwayFromCore(float height, float duration, AnimationCurve curve)
        {
            if (_hoverTween == null)
            {
                _hoverTween = StartCoroutine(HoverInNormalDirection(height, duration, curve));
            }
        }

        /// <summary>
        /// Vanishing coroutine to make this piece sink into floor after being blown off.
        /// </summary>
        /// <param name="duration">How long should it take to sink in and get Destroy() called.</param>
        /// <param name="speed"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Hovering coroutine.
        /// </summary>
        /// <param name="height">height to hover to</param>
        /// <param name="duration">How long from start to end should it take</param>
        /// <param name="curve">Curve along which to animate</param>
        /// <returns>Not used.</returns>
        private IEnumerator HoverInNormalDirection(float height, float duration, AnimationCurve curve)
        {
            if (!(duration >= 0.1f)) yield break;

            var timer = 0f;
            var oldPosition = transform.localPosition;
            var newPosition = oldPosition + _normal * height;
            while (timer < duration)
            {
                timer += WorldStateMachine.Instance.DeltaTime;
                transform.localPosition = Vector3.Lerp(oldPosition, newPosition, curve.Evaluate(timer / duration));
                yield return null;
            }

            transform.localPosition = oldPosition; // Just to be sure
            _hoverTween = null;
        }

        /// <summary>
        /// Rotation speed steen towards new rotation speed.
        /// </summary>
        /// <param name="newSpeed">Value to tween into</param>
        /// <param name="duration">How long should the tween take.</param>
        /// <returns></returns>
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
