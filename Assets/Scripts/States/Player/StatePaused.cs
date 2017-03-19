using Assets.Scripts.States.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.world;
using UnityEngine;
using fi.tamk.hellgame.input;

namespace fi.tamk.hellgame.states.player
{
    class StatePaused : InputTakingState
    {
        public StatePaused(ActorComponent controlledHero) : base(controlledHero)
        {
        }

        public override void HandleInput(float deltaTime)
        {
            if (MyInputController.PollButtonDown(Buttons.ButtonScheme.Pause))
            {
                fi.tamk.hellgame.world.RoomIdentifier.PauseGame();
                ControlledActor.ToPreviousState();
            }
        }

        public override InputStates StateId
        {
            get
            {
                return InputStates.Paused;
            }
        }
    }
}
