using System;
using System.Collections;
using fi.tamk.hellgame.world;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace fi.tamk.hellgame.ui
{
    public class BottomHUD : MonoBehaviour
    {

        [SerializeField] private float Duration = 5f;
        [SerializeField] private GameObject BottomHudPrefab;
        [SerializeField] private float FadeInLenght = 5f;
        [SerializeField] private AnimationCurve FadeInCurve;
        [SerializeField] private float FadeOutLenght = 5f;
        [SerializeField] private AnimationCurve FadeOutCurve;

        private CanvasScaler _canvasScaler;
        private bool _initialized = false;
        private Transform _canvasTransform;
        private GameObject _hud;
        private Coroutine appearingCoroutine;

        public void Init()
        {
            var references = FindObjectOfType<GUIReferences>() ?? new UnityException("GUIReference MonoBehaviour component could not be found in the scene!").Throw<GUIReferences>();
            if (BottomHudPrefab == null) throw new UnityException("Prefab for BottomHUD not set in " + gameObject);
            _canvasScaler = references.Scaler;
            _initialized = true;
            _canvasTransform = references.transform;
        }

        public void DisplayMessage(string message, float duration = 0f, AnimationCurve curve = null)
        {
            if (!_initialized) Init();
            _hud = Instantiate(BottomHudPrefab, _canvasTransform);
            appearingCoroutine = StartCoroutine(MakeBottomHudAppear(_hud, message, FadeInLenght, FadeInCurve));
        }

        public void MakeMessageDisappear()
        {
            if (appearingCoroutine != null) StopCoroutine(appearingCoroutine);

            StartCoroutine(MakeBottomHudDisappear(_hud, FadeOutLenght, FadeOutCurve));
        }

        private IEnumerator MakeBottomHudAppear(GameObject hud, string message, float duration, AnimationCurve fadeInCurve)
        {
            var width = _canvasScaler.referenceResolution.y;
            var text = hud.GetComponentInChildren<TextMeshProUGUI>();
            var image = hud.GetComponentInChildren<Image>();

            text.text = message;

            var timer = 0f;

            timer = 0f;
            yield return null;
            while (timer < duration)
            {
                timer += Time.deltaTime;

                var ratio = timer / duration;
                var col = image.color;
                col.a = Mathf.Lerp(0, 1, fadeInCurve.Evaluate(ratio));
                image.color = col;

                col = text.color;
                col.a = Mathf.Lerp(0, 1, fadeInCurve.Evaluate(ratio));
                text.color = col;

                yield return null;
            }
        }

        private IEnumerator MakeBottomHudDisappear(GameObject hud, float duration, AnimationCurve fadeOutAnimationCurve)
        {
            var text = hud.GetComponentInChildren<TextMeshProUGUI>();
            var image = hud.GetComponentInChildren<Image>();

            var timer = 0f;

            timer = 0f;
            while (timer < duration)
            {
                timer += WorldStateMachine.Instance.DeltaTime;

                var ratio = timer / duration;
                var col = image.color;
                col.a = Mathf.Lerp(1, 0, ratio);
                image.color = col;

                col = text.color;
                col.a = Mathf.Lerp(1, 0, ratio);
                text.color = col;

                yield return null;
            }

            hud.SetActive(false);
            Destroy(hud);            
        }
    }
}