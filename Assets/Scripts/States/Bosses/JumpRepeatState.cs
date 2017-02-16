using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;

namespace fi.tamk.hellgame.states
{
    class JumpRepeatState : StateAbstract
    {
        public JumpRepeatState(ActorComponent controlledHero) : base(controlledHero)
        {

        }

        public override InputStates StateId
        {
            get { return InputStates.JumpRepeat; }
        }

        protected override void CheckForFalling()
        {
            base.CheckForFalling();
        }

        public override void HandleInput(float deltaTime)
        {
            base.HandleInput(deltaTime);
            ControlledActor.GoToState(new SlimeJumpingState(ControlledActor));
        }
    }
}
