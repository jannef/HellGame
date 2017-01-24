using fi.tamk.hellgame.interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using fi.tamk.hellgame.character;
using System;

namespace fi.tamk.hellgame.states
{
    public abstract class StateAbstract : IInputState
    {
        public virtual HeroController ControlledCharacter { get; private set; }

        public CharacterController HeroAvatar
        {
            get { return _heroAvatar ?? (_heroAvatar = ControlledCharacter.CharacterController); }
        }
        private CharacterController _heroAvatar;

        public CharacterStats HeroStats
        {
            get { return _heroStats ?? (_heroStats = ControlledCharacter.HeroStats); }
        }
        private CharacterStats _heroStats;

        protected float _stateTime;

        public abstract InputStates StateID { get; }

        public virtual float StateTimer
        {
            get
            {
                return _stateTime;
            }
        }

        public virtual TransitionType CheckTransitionLegality(InputStates toWhichState)
        {
            return TransitionType.Illegal;
        }

        public virtual void HandleInput(float deltaTime)
        {
            _stateTime += deltaTime;
        }

        public virtual void OnEnterState()
        {
        }

        public virtual void OnExitState()
        {
        }

        public virtual void OnResumeState()
        {
        }

        public virtual void OnSuspendState()
        {
        }

        protected StateAbstract(HeroController controlledHero)
        {
            _stateTime = 0f;
            ControlledCharacter = controlledHero;
        }
    }
}
