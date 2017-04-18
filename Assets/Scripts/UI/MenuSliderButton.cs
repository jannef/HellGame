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
            if (_previousFrameState != PreviousFrameSlidingState.Right)
            {
                _slider.value += _baseSlidingSpeed * Time.unscaledDeltaTime;
                slideTimer = 0;
                _previousFrameState = PreviousFrameSlidingState.Right;
            } else
            {
                slideTimer += Time.unscaledDeltaTime;
                _slider.value += Mathf.Lerp(_baseSlidingSpeed, _topSlidingSpeed, _slidingEasing.Evaluate(slideTimer / _slidingAcceleration)) 
                    * Time.unscaledDeltaTime;
            }
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
            if (_previousFrameState != PreviousFrameSlidingState.Left)
            {
                _slider.value -= _baseSlidingSpeed * Time.unscaledDeltaTime;
                slideTimer = 0;
                _previousFrameState = PreviousFrameSlidingState.Left;
            }
            else
            {
                slideTimer += Time.unscaledDeltaTime;
                _slider.value -= Mathf.Lerp(_baseSlidingSpeed, _topSlidingSpeed, _slidingEasing.Evaluate(slideTimer / _slidingAcceleration))
                    * Time.unscaledDeltaTime;
            }
            StopAllCoroutines();
            StartCoroutine(WaitToNextFrame());
        }
    }
}
