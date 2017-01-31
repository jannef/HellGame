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
        public virtual ActorComponent ControlledActor { get; private set; }

        public CharacterController HeroAvatar
        {
            get { return _heroAvatar ?? (_heroAvatar = ControlledActor.CharacterController); }
        }
        private CharacterController _heroAvatar;

        public HealthComponent ControllerHealth
        {
            get { return _heroStats ?? (_heroStats = ControlledActor.HeroStats); }
        }
        private HealthComponent _heroStats;

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

        protected StateAbstract(ActorComponent controlledHero)
        {
            _stateTime = 0f;
            ControlledActor = controlledHero;
        }

        public virtual bool TakeDamage(int howMuch)
        {
            ControllerHealth.Health -= howMuch;
            if (ControllerHealth.Health > 0) return true;
            ControllerHealth.Die();
            return false;
        }
    }
}
