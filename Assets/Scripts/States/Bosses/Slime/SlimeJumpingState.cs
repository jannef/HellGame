using System;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.utils;
using UnityEngine;
using fi.tamk.hellgame.dataholders;

namespace fi.tamk.hellgame.states
{
    public class SlimeJumpingState : StateAbstract
    {
        protected Transform TargetTransform;
        private Transform Self;

        private float _radius;
        private float _windUpTime = 2;
        private float _jumpHeight = 10f;
        private float _jumpingSpeed = 50f;
        private float _desiredJumpLenght = 1f;
        private float _desiredJumpLenghtEfficiency;
        private AnimationCurve _windUpSquishCurve;

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

        public SlimeJumpingState(ActorComponent controlledHero, Transform target, SlimeJumpData jumpData, Transform self) : base(controlledHero)
        {
            Self = self;
            _startSize = Self.localScale;
            _windUpSquishCurve = jumpData.SquishCurve;
            _startingPosition = ControlledActor.transform.position;
            _scaleChangeStartY = ControlledActor.transform.position.y;
            _minSize = new Vector3(Self.localScale.x * 2f, Self.localScale.y * .5f, Self.localScale.z * 2f);
            _jumpStretchScale = new Vector3(Self.localScale.x * .75f, Self.localScale.y * 1.25f, Self.localScale.z * .75f);
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
                Self.localScale = Vector3.Lerp(_startSize, _minSize, ControlledActor.ActorNumericData.CurveData[2].Evaluate(windUpRatio));
                 ControlledActor.transform.position = new Vector3(ControlledActor.transform.position.x,
                    Mathf.Lerp(_scaleChangeStartY, _scaleChangeStartY - (((_startSize.y - _minSize.y)) / 2),
                    _windUpSquishCurve.Evaluate(windUpRatio)),
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
                    Self.localScale = _startSize;
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

            Self.localScale = Vector3.Lerp(_startSize, _jumpStretchScale,
                ControlledActor.ActorNumericData.CurveData[0].Evaluate(1f - Mathf.Abs(1f - ratio * 2)));

            var vec = Vector3.Lerp(_startingPosition, _endPosition, ratio);
            vec.y = Mathf.Lerp(_startingPosition.y, _startingPosition.y + _jumpHeight,
                ControlledActor.ActorNumericData.CurveData[0].Evaluate(1f - Mathf.Abs(1f - ratio*2)));

            ControlledActor.transform.position = vec;

            if (ratio >= 1) EndJump();
        }

        protected void EndJump()
        {
            ControlledActor.transform.position = _endPosition;
            Self.localScale = _startSize;

            Utilities.DisplacingDamageSplash(ControlledActor.transform, _radius, 1); 

            ControlledActor.ToPreviousState();
        }

        protected virtual void AtJumpEnd()
        {
            _animationController.SetTrigger(Constants.SlimeLandAnimationTrigger);
        }

        public override InputStates StateId
        {
            get { return InputStates.SlimeJumping; }
        }
    }
}
