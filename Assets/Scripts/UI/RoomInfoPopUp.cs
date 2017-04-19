using fi.tamk.hellgame.dataholders;
using fi.tamk.hellgame.world;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        [SerializeField] private TextMeshProUGUI _roomName;
        [SerializeField] private TextMeshProUGUI _topTimeText;
        [SerializeField] private Image _rankImage;
        [SerializeField] private RankSprites _rankSprites;
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

            var transitionTriggers = FindObjectsOfType<RoomPopUpTrigger>();

            foreach (RoomPopUpTrigger trigger in transitionTriggers)
            {
                trigger.PlayerEnterEvent += StartPopUp;
                trigger.PlayerExitTriggerEvent += RemovePopUp;
            }
        }

        public void StartPopUp(RoomPopUpData popUpData)
        {

            _roomName.text = LocaleStrings.LocalizedStringFromEnum(popUpData.roomName);

            var RoomSaveData = UserStaticData.GetRoomData((int)popUpData.roomIndex);

            if (RoomSaveData != null)
            {
                _topTimeText.text = GameClock.FormatTime(System.TimeSpan.FromSeconds(RoomSaveData.RecordTime));
                ClearingRank rank = popUpData.roomRankData.GetRankFromTime(RoomSaveData.RecordTime);
                _rankImage.sprite = _rankSprites.ReturnSpriteByRank(rank);
            } else
            {
                _topTimeText.text = "--:--:--";
                _rankImage.sprite = _rankSprites.ReturnSpriteByRank(ClearingRank.None);
            }

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
