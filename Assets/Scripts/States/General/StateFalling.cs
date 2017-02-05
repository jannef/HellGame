using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.utils;
using fi.tamk.hellgame.utils.Stairs.Utils;
using UnityEngine;

namespace fi.tamk.hellgame.states
{
    class StateFalling : StateAbstract
    {

        public StateFalling(ActorComponent controlledHero) : base(controlledHero)
        {
            DamageMultiplier = 1f;
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

        protected override void CheckForFalling()
        {
        }

        public override InputStates StateId
        {
            get { return InputStates.Falling; }
        }

        public override void HandleInput(float deltaTime)
        {
            base.HandleInput(deltaTime);
            HeroAvatar.Move(Vector3.down * 10f * deltaTime);

            if (StateTime > Constants.FallingDeathLenght)
            {
                Pool.Instance.GetHealthComponent(ControlledActor.gameObject).TakeDisplacingDamage(1);
            }
        }
    }
}
