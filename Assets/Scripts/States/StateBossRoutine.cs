using System;
using System.Collections;
using System.Collections.Generic;
using fi.tamk.hellgame.interfaces;
using UnityEngine;
using fi.tamk.hellgame.character;

namespace fi.tamk.hellgame.states
{
    public class StateBossRoutine : StateAbstract
    {
        public delegate void IndependentObjectEnable(object sender);
        public event IndependentObjectEnable EnabledEvent;
        public event IndependentObjectEnable DisabledEvent;

        public override InputStates StateID
        {
            get
            {
                return InputStates.BossRoutine;
            }
        }

        public override TransitionType CheckTransitionLegality(InputStates toWhichState)
        {
            return TransitionType.LegalOneway;
        }

        public StateBossRoutine(ActorComponent controlledHero) 
            :  base(controlledHero)
        {
        }

        protected override void CheckForFalling()
        {
            
        }

        public override void HandleInput(float deltaTime)
        {
            base.HandleInput(deltaTime);
        }

        public override void OnEnterState()
        {
            base.OnEnterState();
            if (EnabledEvent == null) return;
            EnabledEvent.Invoke(this);
        }

        public override void OnExitState()
        {
            base.OnExitState();
            if (DisabledEvent == null) return;
            DisabledEvent.Invoke(this);
        }
    }
}
