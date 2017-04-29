using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.ui
{

    public class UIGenericPopUp : MonoBehaviour
    {
        private enum CurrentState
        {
            Retracting, StartingToShow, Inactive
        }

        private Vector2 endPosition;
        [SerializeField]
        private Vector2 offSet;
        private Vector2 startingPosition;
        [SerializeField]
        private RectTransform _rectTransform;
        [SerializeField]
        private float movementLenght;
        [SerializeField]
        private AnimationCurve movementCurve;
        [SerializeField]
        private float retractionLenght;
        [SerializeField]
        private AnimationCurve retractionCurve;

        private CurrentState _state = CurrentState.Inactive;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            startingPosition = _rectTransform.anchoredPosition;
            endPosition = startingPosition + offSet;
            _rectTransform.anchoredPosition = endPosition;
        }

        public void StartPopUp()
        {
            StopAllCoroutines();
            if (_state != CurrentState.StartingToShow)
            {
                StartCoroutine(MoveCanvas(_rectTransform, movementLenght, endPosition, startingPosition, movementCurve));
                _state = CurrentState.StartingToShow;
            }
            
        }

        public void RemovePopUp()
        {
            StopAllCoroutines();
            if (_state != CurrentState.Retracting)
            {
                StartCoroutine(MoveCanvas(_rectTransform, retractionLenght, _rectTransform.anchoredPosition, endPosition, retractionCurve));
                _state = CurrentState.Retracting;
            }
        }

        private IEnumerator MoveCanvas(RectTransform rect, float lenght, Vector3 startPosition, Vector3 endPosition, AnimationCurve curve)
        {
            var t = 0f;

            while (t <= 1)
            {
                t += Time.unscaledDeltaTime / lenght;
                rect.anchoredPosition = Vector3.Lerp(startPosition, endPosition, curve.Evaluate(t));
                yield return null;
            }

            rect.anchoredPosition = endPosition;
            _state = CurrentState.Inactive;
            yield return null;
        }
    }
}
