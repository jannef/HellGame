using System.Collections;
using System.Threading;
using fi.tamk.hellgame.world;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace fi.tamk.hellgame.ui
{
    public class RoomNameDisplay : MonoBehaviour
    {
        [SerializeField] private float Duration = 5f;
        [SerializeField] private AnimationCurve TextAnimation;
        private CanvasScaler _canvasScaler;
        private GameObject _roomNameBar;
        private TextMeshProUGUI _roomNameText;
        private RectTransform _rect;

        public void Init(string roomName)
        {
            var references = FindObjectOfType<GUIReferences>() ?? new UnityException("GUIReference MonoBehaviour component could not be found in the scene!").Throw<GUIReferences>();
            _roomNameBar = references.RoomNameBar;
            _roomNameText = references.RoomNameText;
            _roomNameText.text = roomName;
            _rect = _roomNameText.rectTransform;
            _canvasScaler = references.Scaler;
        }

        public void DisplayRoomName()
        {
            StartCoroutine(MoveText(Duration));
        }

        private IEnumerator MoveText(float duration)
        {
            _roomNameBar.SetActive(true);
            Debug.Log(_roomNameText.flexibleWidth);
            var width = (_canvasScaler.referenceResolution.y);

            var timer = 0f;
            while (timer < duration)
            {
                timer += WorldStateMachine.Instance.DeltaTime;
                var ratio = Mathf.Lerp(-1.5f * width, 1.5f * width, TextAnimation.Evaluate(timer / duration));
                _rect.anchoredPosition = new Vector2(ratio, 0);
                yield return null;
            }

            _roomNameBar.SetActive(false);
        }
    }
}