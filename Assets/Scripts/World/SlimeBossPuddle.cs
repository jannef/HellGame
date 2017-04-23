using fi.tamk.hellgame.utils;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace fi.tamk.hellgame.world
{
    public class SlimeBossPuddle : MonoBehaviour
    {
        public float lifeTime;
        public float fadeDuration;
        public AnimationCurve fadeCurve;
        private Renderer _renderer;
        public float _finalScaleMultiplier;
        private Vector3 _finalLocalScale;
        private Vector3 _originalLocalScale;

        private Color _startColor;

        private float _startAcidRadius;
        private float _acidEmissionPerAreaUnit;
        private ParticleSystem _acidSystem;
        private MethodInfo _shapeRadiusMehtod;
        private MethodInfo _emissionRateMethod;

        public void InitializePuddle()
        {
            _originalLocalScale = transform.localScale;
            _finalLocalScale = transform.localScale * _finalScaleMultiplier;
            _renderer = GetComponent<Renderer>();
            _startColor = _renderer.material.color;
            StartCountdownToDeath();

            _acidSystem = GetComponentInChildren<ParticleSystem>();
            _startAcidRadius = _acidSystem.shape.radius;
            _acidEmissionPerAreaUnit = _acidSystem.emission.rateOverTimeMultiplier / _startAcidRadius.AreaFromRadius();

            _shapeRadiusMehtod =  typeof(ParticleSystem.ShapeModule).GetMethod("SetRadius",
                BindingFlags.NonPublic | BindingFlags.Static);
           _emissionRateMethod = typeof(ParticleSystem.EmissionModule).GetMethod("set_rateOverTimeMultiplier");
        }

        private void StartCountdownToDeath()
        {
            StartCoroutine(StaticCoroutines.DoAfterDelay(lifeTime, StartFade));
        }

        private void StartFade()
        {
            StartCoroutine(PuddleDisappearingCoroutine());
        }

        public void RefreshPuddle(float howMuch, float howFast)
        {
            StopAllCoroutines();
            _renderer.material.color = _startColor;
            StartCoroutine(Grow(howMuch, howFast));
        }

        private IEnumerator PuddleDisappearingCoroutine()
        {
            var t = 0f;
            var invisibleColor = new Color(_startColor.r, _startColor.g, _startColor.b, 0);
            var startLocalScale = transform.localScale;

            while (t <= fadeDuration)
            {
                t += WorldStateMachine.Instance.DeltaTime;
                var ratio = t / fadeDuration;
                _renderer.material.color = Color.Lerp(_startColor, invisibleColor, fadeCurve.Evaluate(ratio));
                SetLocalScale( Vector3.Lerp(startLocalScale, _finalLocalScale, fadeCurve.Evaluate(ratio)) );
                yield return null;
            }

            //_renderer.material.color = invisibleColor;
            Destroy(gameObject);
        }

        
        /// <summary>
        /// Grows the puddle, and resets it to the starting color, in case it had already started to disappear.
        /// </summary>
        /// <param name="howMuch">How much to increase, based on the original size.</param>
        /// <param name="duration">How long should the increase take.</param>
        /// <returns>Not used.</returns>
        private IEnumerator Grow(float howMuch, float duration)
        {
            var timer = 0f;
            var startLocalScale = transform.localScale;
            var targetLocalScale = transform.localScale + _originalLocalScale * howMuch;
            var currentColor = _renderer.material.color;

            while (timer < duration)
            {
                timer += WorldStateMachine.Instance.DeltaTime;
                var ratio = timer / duration;
                _renderer.material.color = Color.Lerp(currentColor, _startColor, ratio);
                SetLocalScale( Vector3.Lerp(startLocalScale, targetLocalScale, ratio) );
                yield return null;
            }

            StartCountdownToDeath();
        }

        private void OnDestroy()
        {
            if (SlimeBossPuddleSpawner.Puddles.Contains(this)) SlimeBossPuddleSpawner.Puddles.Remove(this);
        }

        public void SetLocalScale(Vector3 localScaleToSet)
        {
            transform.localScale = localScaleToSet;
            var acidRadius = _startAcidRadius * (localScaleToSet.x / _originalLocalScale.x);
            var acidEmission = acidRadius.AreaFromRadius() * _acidEmissionPerAreaUnit;

            _shapeRadiusMehtod.Invoke(null, new object[] {_acidSystem, acidRadius});
            _emissionRateMethod.Invoke(_acidSystem.emission, new object[] { acidEmission });
        }
    }
}
