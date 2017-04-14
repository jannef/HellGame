using fi.tamk.hellgame.utils;
using System.Collections;
using System.Collections.Generic;
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

        // Use this for initialization
        void Start()
        {
            _finalLocalScale = transform.localScale * _finalScaleMultiplier;
            _renderer = GetComponent<Renderer>();
            StartCoroutine(StaticCoroutines.DoAfterDelay(lifeTime, StartFade));
        }

        void StartFade()
        {
            StartCoroutine(PuddleDisappearingCoroutine());
        }

        private IEnumerator PuddleDisappearingCoroutine()
        {
            var t = 0f;
            var startColor = _renderer.material.color;
            var invisibleColor = new Color(startColor.r, startColor.g, startColor.b, 0);
            var startLocalScale = transform.localScale;

            while (t <= lifeTime)
            {
                t += WorldStateMachine.Instance.DeltaTime;
                var ratio = t / lifeTime;
                _renderer.material.color = Color.Lerp(startColor, invisibleColor, fadeCurve.Evaluate(ratio));
                transform.localScale = Vector3.Lerp(startLocalScale, _finalLocalScale, fadeCurve.Evaluate(ratio));
                yield return null;
            }

            _renderer.material.color = invisibleColor;
            Destroy(gameObject);
            yield return null;
        }
    }
}
