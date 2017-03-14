using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.utils;
using UnityEngine;
using fi.tamk.hellgame.dataholders;
using UnityEditor.Animations;

namespace fi.tamk.hellgame.states
{
    public class SlimeJumpingState : StateAbstract
    {
        protected Transform TargetTransform;

        private float _radius;
        private float _windUpTime = 2;
        private float _jumpHeight = 10f;
        private float _jumpingSpeed = 50f;
        private float _desiredJumpLenght = 1f;
        private float _desiredJumpLenghtEfficiency;

        private Vector3 _startingPosition;
        private Vector3 _endPosition;
        private float _lenght;
        private float _jumpTimer = 0f;
        private bool _isJumping = false;

        private Vector3 _startSize;
        private Vector3 _minSize;
        private float _scaleChangeStartY;

        private Vector3 _jumpStretchScale;

        private Animator _animationController;
        private float _jumpAnimationLenght;
        private bool _hasStartedJumpAnimation = false;

        public SlimeJumpingState(ActorComponent controlledHero, Transform target, SlimeJumpData jumpData) : base(controlledHero)
        {
            _startSize = ControlledActor.transform.localScale;
            _startingPosition = ControlledActor.transform.position;
            _scaleChangeStartY = ControlledActor.transform.position.y;
            _minSize = new Vector3(ControlledActor.transform.localScale.x * 2f, ControlledActor.transform.localScale.y * .5f, ControlledActor.transform.localScale.z * 2f);
            _jumpStretchScale = new Vector3(ControlledActor.transform.localScale.x * .75f, ControlledActor.transform.localScale.y * 1.25f, ControlledActor.transform.localScale.z * .75f);
            TargetTransform = target;
            _radius = ControlledActor.ActorNumericData.ActorFloatData[3];
            _windUpTime = jumpData.JumpDelay;
            _jumpHeight = jumpData.JumpHeight;
            _jumpingSpeed = jumpData.JumpSpeed;
            _desiredJumpLenght = jumpData.TargetJumpLenght;
            _desiredJumpLenghtEfficiency = jumpData.TargetjumpLenghtStrenght;
            _animationController = ControlledActor.GetComponent<Animator>();
            foreach (AnimationClip clip in _animationController.runtimeAnimatorController.animationClips)
            {
                if (String.Equals(clip.name, Constants.SlimeJumpAnimationStateStringName))
                {
                    
                    _jumpAnimationLenght = clip.length * .33f;
                }
            }
        }

        public override void HandleInput(float deltaTime)
        {
            base.HandleInput(deltaTime);

            if (_isJumping)
            {
                CalculatePositionInJumpCurve(deltaTime);
            }
            else
            {
                if (StateTime >= (_windUpTime - _jumpAnimationLenght) && !_hasStartedJumpAnimation)
                {
                    _hasStartedJumpAnimation = true;
                    _animationController.SetTrigger(Constants.SlimeStartJumpAnimationTrigger);
                }


                var windUpRatio = StateTime / _windUpTime;
                ControlledActor.transform.localScale = Vector3.Lerp(_startSize, _minSize, ControlledActor.ActorNumericData.CurveData[2].Evaluate(windUpRatio));
                 ControlledActor.transform.position = new Vector3(ControlledActor.transform.position.x,
                    Mathf.Lerp(_scaleChangeStartY, _scaleChangeStartY - (((_startSize.y - _minSize.y)) / 2), 
                    ControlledActor.ActorNumericData.CurveData[2].Evaluate(windUpRatio)),
                    ControlledActor.transform.position.z);

                if (TargetTransform == null)
                {
                    ControlledActor.ToPreviousState();
                    return;
                }

                var targetVec = new Vector3(TargetTransform.position.x, ControlledActor.transform.position.y, TargetTransform.position.z);

                ControlledActor.transform.LookAt(targetVec);

                if (windUpRatio >= 1f)
                {
                    ControlledActor.transform.position = _startingPosition;
                    ControlledActor.transform.localScale = _startSize;
                    // TODO add support to non-flat surfaces
                    targetVec.x = Mathf.Clamp(targetVec.x, ServiceLocator.WorldLimits[0] + _radius, ServiceLocator.WorldLimits[1] - _radius);
                    targetVec.z = Mathf.Clamp(targetVec.z, ServiceLocator.WorldLimits[2] + _radius, ServiceLocator.WorldLimits[3] - _radius);
                    _endPosition = targetVec;
                    StartJump();
                }
            }

        }

        protected void StartJump()
        {
            effector.Effector.ScreenShakeEffect(new float[2] { 28f, .15f });
            _isJumping = true;
            _startingPosition = ControlledActor.transform.position;
            // TODO clamp to area size. Get size in ServiceLocator

            _lenght = (((_startingPosition - _endPosition).magnitude * (1-_desiredJumpLenghtEfficiency)) + (_desiredJumpLenght *_desiredJumpLenghtEfficiency));
        }

        private void CalculatePositionInJumpCurve(float deltaTime)
        {
            _jumpTimer += deltaTime;
            // TODO: Easing
            var ratio = ControlledActor.ActorNumericData.CurveData[1].Evaluate(((_jumpTimer * _jumpingSpeed) / _lenght));

            ControlledActor.transform.localScale = Vector3.Lerp(_startSize, _jumpStretchScale,
                ControlledActor.ActorNumericData.CurveData[0].Evaluate(1f - Mathf.Abs(1f - ratio * 2)));

            var vec = Vector3.Lerp(_startingPosition, _endPosition, ratio);
            vec.y = Mathf.Lerp(_startingPosition.y, _startingPosition.y + _jumpHeight,
                ControlledActor.ActorNumericData.CurveData[0].Evaluate(1f - Mathf.Abs(1f - ratio*2)));

            ControlledActor.transform.position = vec;

            if (ratio >= 1) EndJump();
        }

        protected override void CheckForFalling()
        {
        }

        protected void EndJump()
        {
            ControlledActor.transform.position = _endPosition;
            ControlledActor.transform.localScale = _startSize;

            var cols = Physics.OverlapSphere(ControlledActor.transform.position, _radius, LayerMask.GetMask(Constants.PlayerLayerName, Constants.PlayerDashingLayerName));

            if (cols.Length > 0)
            {
                cols.ForEach(x => Pool.Instance.GetHealthComponent(x.gameObject).TakeDisplacingDamage(1)); 
            }

            AtJumpEnd();

            ControlledActor.ToPreviousState();
        }

        protected virtual void AtJumpEnd()
        {
            _animationController.SetTrigger(Constants.SlimeLandAnimationTrigger);
            effector.Effector.ScreenShakeEffect(new float[2] { 33f, .44f });
        }

        public override InputStates StateId
        {
            get { return InputStates.SlimeJumping; }
        }
    }
}
