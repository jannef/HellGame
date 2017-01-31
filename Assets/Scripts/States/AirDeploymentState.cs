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
        private InputStates _startingStateID;
        private float _fallingDuration = 3f;
        private AnimationCurve _animationCurve;
        private Vector3 _landingPosition;


        public AirDeploymentState(ActorComponent controlledHero, InputStates startingStateID, float fallingDuration, 
            AnimationCurve fallingCurve, Vector3 fallingDirection) : base(controlledHero)
        {
            _startingStateID = startingStateID;
            _fallingDuration = fallingDuration;
            _animationCurve = fallingCurve;

            var ray = new Ray(ControlledActor.transform.position, fallingDirection);
            RaycastHit hit;

            Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity,
                LayerMask.GetMask(new string[] {Constants.GroundRaycastLayerName, Constants.ObstacleLayerName}));

            _landingPosition = hit.point;

        }

        public override InputStates StateID
        {
            get { return InputStates.AirDeploymentState; }
        }

        protected override void CheckForFalling()
        {
            
        }

        public override void HandleInput(float deltaTime)
        {
            base.HandleInput(deltaTime);
        }
    }
}
