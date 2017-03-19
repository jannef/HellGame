using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.input;
using fi.tamk.hellgame.states;
using UnityEngine;
using fi.tamk.hellgame.states.player;

namespace Assets.Scripts.States.Player
{
    public abstract class InputTakingState : StateAbstract
    {
        protected InputController MyInputController;
        protected Animator CharacterAnimator;

        protected InputTakingState(ActorComponent controlledHero) : base(controlledHero)
        {
            MyInputController = ControlledActor.gameObject.GetComponent<InputController>() ?? new UnityException("Input handling state can't find InputController in the player GameObject!").Throw<InputController>();
            CharacterAnimator = ControlledActor.gameObject.GetComponentInChildren<Animator>();
        }

        public override void HandleInput(float deltaTime)
        {
            base.HandleInput(deltaTime);
            CheckForUniversalInputs();
        }

        protected void CheckForUniversalInputs()
        {
            if (MyInputController.PollButtonDown(Buttons.ButtonScheme.Pause))
            {
                fi.tamk.hellgame.world.RoomIdentifier.PauseGame();
                ControlledActor.GoToState(new StatePaused(ControlledActor));
            }
        }
    }
}
