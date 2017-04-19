using UnityEngine;
using System.Collections;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using System;

namespace fi.tamk.hellgame.states
{
    public class ChambersIntermission : ChambersBase
    {
        private float _duration;
        private ChambersBase _nextState;
        private bool _triggered = false;

        public ChambersIntermission(ActorComponent controlledHero, ChambersBase next, float duration, ChambersBase clonedState = null)
            : base(controlledHero, clonedState)
        {
            _duration = duration;
            _nextState = next;
        }

        public override void HandleInput(float deltaTime)
        {
            if (_triggered) return;
            base.HandleInput(deltaTime);

            if (StateTime > _duration)
            {
                _triggered = true;
                ControlledActor.GoToState(_nextState);
            }
        }

        public override void OnEnterState()
        {
            NavigationAgent.SetDestination(StartPosition);
            NavigationAgent.speed = StartSpeed * 5f;
        }

        public override void OnExitState()
        {
            NavigationAgent.speed = StartSpeed;
        }

        public override InputStates StateId
        {
            get
            {
                return InputStates.ChambersIntermission;
            }
        }
    }
}
