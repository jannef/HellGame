using UnityEngine;
using System.Collections;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using System;

namespace fi.tamk.hellgame.states
{
    public class ChambersIntermission : ChambersBase
    {
        private bool _triggered = false;

        public ChambersIntermission(ActorComponent controlledHero, ChambersBase clonedState = null)
            : base(controlledHero, clonedState)
        {

        }

        public override void HandleInput(float deltaTime)
        {
            if (_triggered) return;
            base.HandleInput(deltaTime);
        }

        public override void OnEnterState()
        {
            NavigationAgent.SetDestination(ControlledActor.transform.position);
            NavigationAgent.speed = 0f;
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
