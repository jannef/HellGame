using fi.tamk.hellgame.dataholders;
using fi.tamk.hellgame.world;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace fi.tamk.hellgame.ui
{
    public delegate void RoomPopUpAction(RoomPopUpData data);

    public class RoomInfoPopUp : MonoBehaviour
    {
        private Vector2 endPosition;
        [SerializeField] private Vector2 offSet;
        private Vector2 startingPosition;
        private CanvasGroup _canvasGroup;
        private Image _roomImage;
        private Text _roomName;
        [SerializeField] private Text _roomTopClearTime;
        private RectTransform _rectTransform;
        [SerializeField] private float movementLenght;
        [SerializeField] private AnimationCurve movementCurve;
        [SerializeField] private float retractionLenght;
        [SerializeField] private AnimationCurve retractionCurve;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            startingPosition = _rectTransform.anchoredPosition;
            endPosition = startingPosition + offSet;
            _rectTransform.anchoredPosition = endPosition;
            _canvasGroup = GetComponent<CanvasGroup>();
            _roomImage = GetComponentInChildren<Image>();
            _roomName = GetComponentInChildren<Text>();

            var transitionTriggers = FindObjectsOfType<RoomPopUpTrigger>();

            foreach (RoomPopUpTrigger trigger in transitionTriggers)
            {
                trigger.PlayerEnterEvent += StartPopUp;
                trigger.PlayerExitTriggerEvent += RemovePopUp;
            }
        }

        public void StartPopUp(RoomPopUpData popUpData)
        {

            if (popUpData.popUpPicture != null)
            {
                _roomImage.sprite = popUpData.popUpPicture;
            }

            var stats = UserStaticData.GetRoomData(popUpData.roomIndex);
            System.TimeSpan completionTime;

            if (stats != null)
            {
                completionTime = System.TimeSpan.FromSeconds(stats.RecordTime);
                
            } else
            {
               completionTime = System.TimeSpan.FromSeconds(0);
            }
            _roomTopClearTime.text = string.Format("{0}:{1}:{2}", completionTime.Minutes, completionTime.Seconds, completionTime.Milliseconds);
            _roomName.text = popUpData.roomName;

            StopAllCoroutines();
            StartCoroutine(MoveCanvas(_rectTransform, movementLenght, endPosition, startingPosition, movementCurve));
        }

        public void RemovePopUp()
        {
            StopAllCoroutines();
            StartCoroutine(MoveCanvas(_rectTransform, retractionLenght, _rectTransform.anchoredPosition, endPosition, retractionCurve));
        }

        private IEnumerator MoveCanvas(RectTransform rect, float lenght, Vector2 startPosition, Vector2 endPosition, AnimationCurve curve)
        {
            var t = 0f;

            while (t <= 1)
            {
                t += Time.deltaTime / lenght;
                rect.anchoredPosition = Vector2.Lerp(startPosition, endPosition, curve.Evaluate(t));
                yield return null;
            }

            rect.anchoredPosition = endPosition;

            yield return null;
        }
    }
}
