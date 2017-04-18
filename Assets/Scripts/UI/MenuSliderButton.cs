using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace fi.tamk.hellgame.ui
{

    public class MenuSliderButton : BasicMenuButton
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private AnimationCurve _slidingEasing;
        [SerializeField] private float _slidingAcceleration;
        [SerializeField] private float _baseSlidingSpeed;
        [SerializeField] private float _topSlidingSpeed;
        private float slideTimer;
        private PreviousFrameSlidingState _previousFrameState = PreviousFrameSlidingState.None;

        private enum PreviousFrameSlidingState
        {
            Left, Right, None
        }

        public override void MovePointerToThis(MenuCommander commander)
        {
            base.MovePointerToThis(commander);
            commander.AddCommand(MenuActionType.Left, AdjustLeft);
            commander.AddCommand(MenuActionType.Right, AdjustRight);
        }

        public void AdjustRight(MenuCommander commander)
        {
            var newValue = _slider.value;

            if (_previousFrameState != PreviousFrameSlidingState.Right)
            {
                newValue = Mathf.Clamp(newValue + _baseSlidingSpeed * Time.unscaledDeltaTime, 0f, 1f);
                slideTimer = 0;
                _previousFrameState = PreviousFrameSlidingState.Right;
            } else
            {
                slideTimer += Time.unscaledDeltaTime;
                newValue = Mathf.Clamp(newValue + Mathf.Lerp(_baseSlidingSpeed, _topSlidingSpeed, _slidingEasing.Evaluate(slideTimer / _slidingAcceleration))
                    * Time.unscaledDeltaTime, 0f, 1f);
            }

            _slider.value = newValue;

            StopAllCoroutines();
            StartCoroutine(WaitToNextFrame());
        }

        private IEnumerator WaitToNextFrame()
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            _previousFrameState = PreviousFrameSlidingState.None;
        }

        public void AdjustLeft(MenuCommander commander)
        {
            var newValue = _slider.value;

            if (_previousFrameState != PreviousFrameSlidingState.Left)
            {
                newValue = Mathf.Clamp(newValue - _baseSlidingSpeed * Time.unscaledDeltaTime, -1f, 1f);
                slideTimer = 0;
                _previousFrameState = PreviousFrameSlidingState.Left;
            }
            else
            {
                slideTimer += Time.unscaledDeltaTime;
                newValue = Mathf.Clamp(newValue - Mathf.Lerp(_baseSlidingSpeed, _topSlidingSpeed, _slidingEasing.Evaluate(slideTimer / _slidingAcceleration))
                    * Time.unscaledDeltaTime, -1f, 1f);
            }

            _slider.value = newValue;

            StopAllCoroutines();
            StartCoroutine(WaitToNextFrame());
        }
    }
}
