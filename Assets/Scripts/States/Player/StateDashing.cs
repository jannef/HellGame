using fi.tamk.hellgame.interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using fi.tamk.hellgame.character;
using System;
using fi.tamk.hellgame.utils;

namespace fi.tamk.hellgame.states
{
    public class StateDashing : StateAbstract
    {
        protected int OriginalLayer;
        private float _dashLenghtMultiplier;

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

            if (StateTime > ControlledActor.DashDuration * _dashLenghtMultiplier)
            {
                // For last frame of the dash, move remaining dash distance and change state back to previous.
                var overTime = (StateTime - ControlledActor.DashDuration * _dashLenghtMultiplier) / ControlledActor.DashDuration *_dashLenghtMultiplier;
                HeroAvatar.Move(_dashingDirection * deltaTime * ControlledActor.DashSpeed * overTime);

                ControlledActor.ToPreviousState();
                return;
            }
            HeroAvatar.Move(_dashingDirection * deltaTime * ControlledActor.DashSpeed);
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
            ControlledActor.gameObject.SetLayer(Constants.PlayerDashingLayer);
        }

        public override void OnExitState()
        {
            ControlledActor.gameObject.SetLayer(OriginalLayer);
        }
    }
}
