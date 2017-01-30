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

        public override InputStates StateID
        {
            get
            {
                return InputStates.Dashing;
            }
        }

        private Vector3 dashingDirection;

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

        public override void HandleInput(float deltaTime)
        {
            _stateTime += deltaTime;

            if (_stateTime > HeroStats.DashDuration)
            {
                // For last frame of the dash, move remaining dash distance and change state back to previous.
                var overTime = (_stateTime - HeroStats.DashDuration) / HeroStats.DashDuration;
                HeroAvatar.Move(dashingDirection * deltaTime * HeroStats.DashSpeed * overTime);

                ControlledCharacter.ToPreviousState();
                return;
            }
            HeroAvatar.Move(dashingDirection * deltaTime * HeroStats.DashSpeed);
        }

        public StateDashing(HeroController controlledHero, Vector3 dashingDirection) : base(controlledHero)
        {
            this.dashingDirection = dashingDirection;
        }

        public override void TakeDamage(int howMuch)
        {
            
        }

        public override void OnEnterState()
        {
            OriginalLayer = ControlledCharacter.gameObject.layer;
            ControlledCharacter.gameObject.SetLayer(Constants.PlayerDashingLayer);
        }

        public override void OnExitState()
        {
            ControlledCharacter.gameObject.SetLayer(OriginalLayer);
        }
    }
}
