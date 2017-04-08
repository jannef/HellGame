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
        [SerializeField] private AnimationCurve TextAnimation;
        [SerializeField] private GameObject BottomHudPrefab;

        private CanvasScaler _canvasScaler;
        private bool _initialized = false;
        private Transform _canvasTransform;

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
            var go = Instantiate(BottomHudPrefab, _canvasTransform);
            StartCoroutine(MoveText(go, message, duration <= 0f ? Duration : duration, curve ?? TextAnimation));
        }

        private IEnumerator MoveText(GameObject hud, string message, float duration, AnimationCurve curve)
        {
            var width = _canvasScaler.referenceResolution.y;
            var text = hud.GetComponentInChildren<TextMeshProUGUI>();
            text.text = message;

            var timer = 0f;
            while (timer < duration)
            {
                timer += WorldStateMachine.Instance.DeltaTime;
                var ratio = Mathf.Lerp(-width, width, curve.Evaluate(timer / duration));
                text.rectTransform.anchoredPosition = new Vector2(ratio, 0);
                yield return null;
            }

            hud.SetActive(false);
            Destroy(hud);
            yield return null;
        }
    }
}