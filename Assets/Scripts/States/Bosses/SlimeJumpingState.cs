using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.utils;
using UnityEngine;

namespace fi.tamk.hellgame.states
{
    class SlimeJumpingState : StateAbstract
    {
        private float _radius;
        private float _windUpTime = 2;
        private float _jumpHeight = 2.3f;
        private float _jumpingSpeed = 15f;

        private Vector3 _startingPosition;
        private Vector3 _endPosition;
        private float _lenght;
        private float _jumpTimer = 0f;
        private bool _isJumping = false;

        public SlimeJumpingState(ActorComponent controlledHero) : base(controlledHero)
        {
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
                var windUpRatio = StateTime / _windUpTime;
                var playerTransform = ServiceLocator.Instance.GetNearestPlayer(ControlledActor.transform.position);
                if (playerTransform == null)
                {
                    ControlledActor.ToPreviousState();
                    return;
                }

                var targetVec = new Vector3(playerTransform.position.x, ControlledActor.transform.position.y, playerTransform.position.z);

                ControlledActor.transform.LookAt(targetVec);

                if (windUpRatio >= 1f)
                {
                    // TODO add support to non-flat surfaces
                    _endPosition = targetVec;
                    Debug.Log(targetVec);
                    StartJump();
                }
            }

        }

        private void StartJump()
        {
            _isJumping = true;
            _startingPosition = ControlledActor.transform.position;
            // TODO clamp to area size. Get size in ServiceLocator

            _lenght = (_startingPosition - _endPosition).magnitude;
        }

        private void CalculatePositionInJumpCurve(float deltaTime)
        {
            _jumpTimer += deltaTime;
            // TODO: Easing
            var ratio = (_jumpTimer * _jumpingSpeed) / _lenght;

            Debug.Log(1 - Mathf.Abs(0.5f - ratio));

            var vec = Vector3.Lerp(_startingPosition, _endPosition, ratio);
            vec.y = Mathf.Lerp(_startingPosition.y, _startingPosition.y + _jumpHeight * _lenght,
                0.5f - Mathf.Abs(0.5f - ratio));

            ControlledActor.transform.position = vec;

            if (ratio >= 1) EndJump();
        }

        protected override void CheckForFalling()
        {
        }

        private void EndJump()
        {
            ControlledActor.transform.position = _endPosition;
            ControlledActor.ToPreviousState();
        }

        public override InputStates StateId
        {
            get { return InputStates.SlimeJumping; }
        }
    }
}
