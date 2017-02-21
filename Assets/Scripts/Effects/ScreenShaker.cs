using UnityEngine;
using System.Collections;
using fi.tamk.hellgame.utils;

namespace fi.tamk.hellgame.effects
{
    public class ScreenShaker : Singleton<ScreenShaker>
    {
        public AnimationCurve ShakeEasing;

        private Transform _camTransform;
        private float _shakeAmount = 0;
        private float _shakeLenght = 0;
        private float _lerpTimer = 0;
        private float _currentShakeAmount = 0;

        Vector3 originalPos;

        protected void Start()
        {
            if (_camTransform == null)
            {
                _camTransform = Camera.main.transform;
            }
        }

        public void Shake(float shakeAmount, float shakeLenght)
        {

            if (_shakeAmount > 0)
            {
                _shakeAmount = Mathf.Max(_currentShakeAmount, shakeAmount);
                _shakeLenght = Mathf.Max(shakeLenght, _shakeLenght - _lerpTimer * _shakeLenght);
                _lerpTimer = 0;
            }
            else
            {
                originalPos = _camTransform.localPosition;
                _shakeAmount = shakeAmount;
                _shakeLenght = shakeLenght;
                StartCoroutine(ShakeRoutine());
            }
        }

        private IEnumerator ShakeRoutine()
        {
            while (_lerpTimer <= 1)
            {
                _camTransform.localPosition = originalPos + Random.insideUnitSphere.normalized * _currentShakeAmount * Time.deltaTime;

                _lerpTimer += Time.deltaTime / _shakeLenght;
                _currentShakeAmount = Mathf.Lerp(_shakeAmount, 0, ShakeEasing.Evaluate(_lerpTimer));
                yield return null;
            }

            StopShaking();

            yield return null;
        }

        protected void StopShaking()
        {
            _lerpTimer = 0f;
            _shakeAmount = 0;
            _shakeLenght = 0;
            _camTransform.localPosition = originalPos;
        }
    }
}
