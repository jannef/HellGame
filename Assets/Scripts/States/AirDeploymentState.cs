using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.utils;
using UnityEngine;

namespace fi.tamk.hellgame.states
{
    class AirDeploymentState : StateAbstract
    {
        private IInputState _startingState;
        private float _fallingDuration = 3f;
        private AnimationCurve _animationCurve;
        private Vector3 _landingPosition;
        private Vector3 _startingPosition;
        private bool _isDeploying = true;


        public AirDeploymentState(ActorComponent controlledHero, IInputState startingState, Vector3 startingPosition, 
            float fallingDuration, AnimationCurve fallingCurve, Vector3 landingCoordinates, Vector3 modelGroundPointLocalPos)
            : base(controlledHero)
        {
            _startingState = startingState;
            _fallingDuration = fallingDuration;
            _animationCurve = fallingCurve;
            _startingPosition = startingPosition;

            var ray = new Ray(landingCoordinates + Vector3.up * 1000f, Vector3.down);
            RaycastHit hit;

            Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity,
                LayerMask.GetMask(new string[] {Constants.GroundRaycastLayerName}));
            _landingPosition = hit.point - controlledHero.transform.localScale.y * modelGroundPointLocalPos;

        }

        public override InputStates StateID
        {
            get { return InputStates.AirDeploymentState; }
        }

        public override TransitionType CheckTransitionLegality(InputStates toWhichState)
        {
            switch (toWhichState)
            {
                case InputStates.Paused:
                    return TransitionType.LegalTwoway;
                default:
                    return TransitionType.LegalOneway;
            }
        }

        protected override void CheckForFalling()
        {
            
        }

        public override void OnExitState()
        {
            base.OnExitState();
            if (_isDeploying) ControlledActor.transform.position = _landingPosition;
        }

        public override void HandleInput(float deltaTime)
        {
            base.HandleInput(deltaTime);

            var newPosition = Vector3.Lerp(_startingPosition, _landingPosition,
                _animationCurve.Evaluate(_stateTime / _fallingDuration));

            ControlledActor.transform.position = newPosition;

            if (_stateTime >= _fallingDuration)
            {
                _isDeploying = true;
                ControlledActor.GoToState(_startingState);
            }
        }
    }
}
