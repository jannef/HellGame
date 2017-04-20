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
        private const float FadeoutDuration = 1f;

        [SerializeField] private float Duration = 5f;
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
            StartCoroutine(MoveText(go, message, duration <= 0 ? Duration : duration));
        }

        private IEnumerator MoveText(GameObject hud, string message, float duration)
        {
            var width = _canvasScaler.referenceResolution.y;
            var text = hud.GetComponentInChildren<TextMeshProUGUI>();
            var image = hud.GetComponentInChildren<Image>();

            text.text = message;

            var timer = 0f;
            while (timer < duration)
            {
                timer += WorldStateMachine.Instance.DeltaTime;
                yield return null;
            }

            timer = 0f;
            while (timer < FadeoutDuration)
            {
                timer += WorldStateMachine.Instance.DeltaTime;

                var ratio = timer / FadeoutDuration;
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