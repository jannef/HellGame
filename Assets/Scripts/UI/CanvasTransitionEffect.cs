using fi.tamk.hellgame.world;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.ui
{

    public class CanvasTransitionEffect : MonoBehaviour
    {
        private CanvasGroup _canvasGroup;
        [SerializeField] private float transitionLenght;
        [SerializeField] private AnimationCurve fadeCurve;

        private void Awake()
        {
            _canvasGroup = GetComponentInParent<CanvasGroup>();
        }

        public void ActivateTransitionEffect()
        {
            StartCoroutine(FadeInRoutine(_canvasGroup, transitionLenght, fadeCurve));
           _canvasGroup.alpha = 0;
        }

        void StopRoutine()
        {
            RoomIdentifier.GameResumed -= StopRoutine;
            StopAllCoroutines();
            _canvasGroup.alpha = 0;
        }

        IEnumerator FadeInRoutine(CanvasGroup canvasGroup, float lenght, AnimationCurve curve)
        {
            RoomIdentifier.GameResumed += StopRoutine;
            var t = 0f;
            while (t < 1)
            {
                t += Time.unscaledDeltaTime / lenght;
                canvasGroup.alpha = curve.Evaluate(t);

                yield return null;
            }
            RoomIdentifier.GameResumed -= StopRoutine;
            canvasGroup.alpha = curve.Evaluate(1);
        }
    }
}
