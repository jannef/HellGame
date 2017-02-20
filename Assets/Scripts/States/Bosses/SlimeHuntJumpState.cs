using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using UnityEngine;

namespace fi.tamk.hellgame.states
{
    class SlimeHuntJumpState : SlimeJumpingState
    {
        public SlimeHuntJumpState(ActorComponent controlledHero, Transform targetTransform) : base(controlledHero, targetTransform)
        {
        }

        public override InputStates StateId
        {
            get
            {
                return InputStates.SlimeHuntJumping;
            }
        }
    }
}
