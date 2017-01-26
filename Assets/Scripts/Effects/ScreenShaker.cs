using UnityEngine;
using System.Collections;
using fi.tamk.hellgame.utils;

namespace fi.tamk.hellgame.effects
{
    public class ScreenShaker : Singleton<ScreenShaker>
    {
        private Transform camTransform;

        private float shakeAmount = 0;
        private float shakeLenght = 0;
        private float lerpTimer = 0;
        private float currentShakeAmount = 0;
        public AnimationCurve shakeEasing;

        Vector3 originalPos;

        void Start()
        {
            if (camTransform == null)
            {
                camTransform = GetComponent<Transform>();
            }
        }

        public void Shake(float shakeAmount, float shakeLenght)
        {

            if (this.shakeAmount > 0)
            {
                this.shakeAmount = currentShakeAmount + shakeAmount;
                this.shakeLenght = Mathf.Max(shakeLenght, this.shakeLenght - lerpTimer * this.shakeLenght);
                lerpTimer = 0;
            }
            else
            {
                originalPos = camTransform.localPosition;
                this.shakeAmount = shakeAmount;
                this.shakeLenght = shakeLenght;
                StartCoroutine(ShakeRoutine());
            }
        }

        private IEnumerator ShakeRoutine()
        {
            while (lerpTimer <= 1)
            {
                camTransform.localPosition = originalPos + Random.insideUnitSphere.normalized * currentShakeAmount * Time.deltaTime;

                lerpTimer += Time.deltaTime / shakeLenght;
                currentShakeAmount = Mathf.Lerp(shakeAmount, 0, shakeEasing.Evaluate(lerpTimer));
                yield return null;
            }

            StopShaking();

            yield return null;
        }

        void StopShaking()
        {
            lerpTimer = 0f;
            shakeAmount = 0;
            shakeLenght = 0;
            camTransform.localPosition = originalPos;
        }
    }
}
