using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using UnityEngine;

namespace fi.tamk.hellgame.states
{
    class StateFalling : StateAbstract
    {
        private const float FallingDeathLenght = 5f;

        public StateFalling(ActorComponent controlledHero) : base(controlledHero)
        {
        }

        public override TransitionType CheckTransitionLegality(InputStates toWhichState)
        {
            switch (toWhichState)
            {
                case InputStates.Paused:
                    return TransitionType.LegalTwoway;
                case InputStates.Dead:
                    return TransitionType.LegalOneway;
                default:
                    return TransitionType.Illegal;
            }
        }

        public override InputStates StateID
        {
            get { return InputStates.Falling; }
        }

        public override void HandleInput(float deltaTime)
        {
            base.HandleInput(deltaTime);
            HeroAvatar.Move(Vector3.down * 10f * deltaTime);
        }
        
        protected override void CheckForFalling()
        {
            if (_stateTime >= FallingDeathLenght)
            {
                ControllerHealth.Die();
            }
        }
    }
}
