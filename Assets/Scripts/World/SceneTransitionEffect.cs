using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace fi.tamk.hellgame.world
{

    public class SceneTransitionEffect : MonoBehaviour
    {
        private CanvasGroup _blackCanvas;
        [SerializeField] private float fadeInLenght;
        [SerializeField] private AnimationCurve fadeInCurve;
        [SerializeField] private float fadeOutLenght;
        [SerializeField] private AnimationCurve fadeOutCurve;
        public UnityEvent SceneTransitionEvent;

        void Start()
        {
            _blackCanvas = GetComponent<CanvasGroup>();
            _blackCanvas.alpha = 1;
            StartCoroutine(FadeOutine());
        }

        public virtual void StartSceneTransition(int sceneToLoad)
        {
            if (SceneTransitionEvent != null) SceneTransitionEvent.Invoke();
            StartCoroutine(FadeInRoutine(sceneToLoad));
            
        }

        IEnumerator FadeOutine()
        {
            var t = 0f;
            yield return null;
            while (t < 1)
            {
                t += Time.unscaledDeltaTime / fadeOutLenght;
                _blackCanvas.alpha = (1 - fadeOutCurve.Evaluate(t));

                yield return null;
            }

            _blackCanvas.alpha = 0;
        }

        protected IEnumerator FadeInRoutine(int sceneToLoad)
        {
            var t = 0f;
            while (t < 1)
            {
                t += Time.unscaledDeltaTime / fadeInLenght;
                _blackCanvas.alpha = fadeInCurve.Evaluate(t);

                yield return null;
            }

            _blackCanvas.alpha = fadeInCurve.Evaluate(1);

            SceneManager.LoadScene(sceneToLoad);
        }


    }
}
