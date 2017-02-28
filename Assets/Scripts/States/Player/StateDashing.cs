using fi.tamk.hellgame.interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using fi.tamk.hellgame.character;
using System;
using fi.tamk.hellgame.dataholders;
using fi.tamk.hellgame.utils;

namespace fi.tamk.hellgame.states
{
    public class StateDashing : StateAbstract
    {
        protected int OriginalLayer;
        private float _dashLenghtMultiplier;
        private TrailRenderer _playerTrail;

        public override InputStates StateId
        {
            get
            {
                return InputStates.Dashing;
            }
        }

        private Vector3 _dashingDirection;

        public override TransitionType CheckTransitionLegality(InputStates toWhichState)
        {
            switch (toWhichState)
            {
                case InputStates.Paused:
                    return TransitionType.LegalTwoway;
                case InputStates.Dead:
                case InputStates.Running:
                    return TransitionType.LegalOneway;
                default:
                    return TransitionType.Illegal;
            }
        }

        protected override void CheckForFalling()
        {
        }

        public override void HandleInput(float deltaTime)
        {
            StateTime += deltaTime;

            if (StateTime > ControlledActor.ActorNumericData.ActorFloatData[(int)ActorDataMap.DashDuration] * _dashLenghtMultiplier)
            {
                // For last frame of the dash, move remaining dash distance and change state back to previous.
                var overTime = (StateTime - ControlledActor.ActorNumericData.ActorFloatData[(int)ActorDataMap.DashDuration] * _dashLenghtMultiplier) / ControlledActor.ActorNumericData.ActorFloatData[(int)ActorDataMap.DashDuration] * _dashLenghtMultiplier;
                HeroAvatar.Move(_dashingDirection * deltaTime * ControlledActor.ActorNumericData.ActorFloatData[(int)ActorDataMap.DashSpeed] * overTime * Constants.SmootherstepDerivateEasing( StateTime / ControlledActor.ActorNumericData.ActorFloatData[(int)ActorDataMap.DashDuration]));

                ControlledActor.ToPreviousState();
                return;
            }
            HeroAvatar.Move(_dashingDirection * deltaTime * ControlledActor.ActorNumericData.ActorFloatData[(int)ActorDataMap.DashSpeed] * Constants.SmootherstepDerivateEasing(StateTime / ControlledActor.ActorNumericData.ActorFloatData[(int)ActorDataMap.DashDuration]));
        }

        public StateDashing(ActorComponent controlledHero, Vector3 dashingDirection, float dashLengthMultiplier = 1f) : base(controlledHero)
        {
            _dashingDirection = dashingDirection;
            _dashLenghtMultiplier = dashLengthMultiplier;
            DamageMultiplier = 0f;
        }

        public override void OnEnterState()
        {
            OriginalLayer = ControlledActor.gameObject.layer;
            ControlledActor.gameObject.SetLayer(Constants.PlayerDashingLayer, false);
        }

        public override void OnExitState()
        {
            ControlledActor.gameObject.SetLayer(OriginalLayer, false);
        }
    }
}
