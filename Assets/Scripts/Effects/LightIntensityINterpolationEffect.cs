using fi.tamk.hellgame.world;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.effect
{

    public class LightIntensityINterpolationEffect : MonoBehaviour
    {
        [SerializeField]
        private float _effectLenght;
        [SerializeField]
        private AnimationCurve _lerpCurve;
        private Light _light;
        [SerializeField] private float startIntensity;
        [SerializeField] private float endIntensity;

        public void StartEffect()
        {
            _light = GetComponent<Light>();
            if (_light == null) return;
            StartCoroutine(ChangeLightIntensityRoutine());
        }

        private IEnumerator ChangeLightIntensityRoutine()
        {
            var t = 0f;

            while (t <= _effectLenght)
            {
                t += WorldStateMachine.Instance.DeltaTime;
                _light.intensity = Mathf.Lerp(startIntensity, endIntensity, _lerpCurve.Evaluate(t / _effectLenght));
                yield return null;
            }

            _light.intensity = endIntensity;
        }
    }
}
