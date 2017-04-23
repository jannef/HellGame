using fi.tamk.hellgame.world;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.effect
{

    public class SizeChangeEffect : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _sizeLerp;
        [SerializeField] private float _changeDuration;
        [SerializeField] private Vector3 _finalSize;
        public bool DestroyAfterEffect = false;

        public void StartChangingSize(float duration = 0f)
        {
            StartCoroutine(SizeChangeCoroutine(duration > 0 ? duration : _changeDuration));
        }

        private IEnumerator SizeChangeCoroutine(float duration)
        {
            var t = 0f;
            var originalScale = transform.localScale;

            while (t <= duration)
            {
                t += WorldStateMachine.Instance.DeltaTime;
                var ratio = t / duration; 
                transform.localScale = Vector3.Lerp(originalScale, _finalSize, _sizeLerp.Evaluate(ratio));
                yield return null;
            }

            transform.localScale = _finalSize;
            if (DestroyAfterEffect)
            {
                Destroy(gameObject);
            }
            yield return null;
        }
    }
}
