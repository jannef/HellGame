using fi.tamk.hellgame.character;
using fi.tamk.hellgame.world;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.effectors
{

    public class IdleRotationRampUp : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _rampUpAnimation;
        [SerializeField] private float _finalSpeed;
        [SerializeField] private float _rampuUpLenght;
        [SerializeField] private bool _rampUpAtStart = false;

        private IdleRotation _idleRotation;
        private Vector3 _originalRotation;

        // Use this for initialization
        void Start()
        {
            _idleRotation = GetComponent<IdleRotation>();
            _originalRotation = transform.localRotation.eulerAngles;
            if (_rampUpAtStart == true)
            {
                StartRampUp();
            }
        }

        public void StartRampUp()
        {
            StartCoroutine(RampUpCoroutine());
        }

        public void ReturnToOriginalRotation()
        {
            StartCoroutine(RotationRoutine());
        }

        private IEnumerator RotationRoutine()
        {
            var t = 0f;
            var startRotation = transform.rotation.eulerAngles;

            while (t < _rampuUpLenght)
            {
                t += WorldStateMachine.Instance.DeltaTime;
                var ratio = t / _rampuUpLenght;
                transform.localRotation = Quaternion.Euler(Vector3.Lerp(startRotation, _originalRotation, _rampUpAnimation.Evaluate(ratio)));
                yield return null;
            }
            transform.localRotation = Quaternion.Euler(_originalRotation);

            yield return null;
        }

        private IEnumerator RampUpCoroutine()
        {
            var t = 0f;
            _idleRotation.enabled = true;

            while (t < _rampuUpLenght)
            {
                t += WorldStateMachine.Instance.DeltaTime;
                var ratio = t / _rampuUpLenght;
                _idleRotation._rotationSpeed = Mathf.Lerp(0, _finalSpeed, _rampUpAnimation.Evaluate(ratio));
                yield return null;
            }

            yield return null;
        }
    }
}
