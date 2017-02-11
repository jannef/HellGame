using fi.tamk.hellgame.interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using fi.tamk.hellgame.character;
using System;
using fi.tamk.hellgame.input;
using tamk.fi.hellgame.character;

namespace fi.tamk.hellgame.states
{
    public class StateRunning : StateAbstract
    {
        private InputController _myController;

        public override InputStates StateId
        {
            get { return InputStates.Running; }
        }

        public override TransitionType CheckTransitionLegality(InputStates toWhichState)
        {
            switch (toWhichState)
            {
                case InputStates.Dead:
                    return TransitionType.LegalOneway;
                case InputStates.Running:
                    return TransitionType.Illegal;
                default:
                    return TransitionType.LegalTwoway;
            }
        }

        public override void HandleInput(float deltaTime)
        {
            base.HandleInput(deltaTime);
            RunningMovement(deltaTime);
        }

        protected virtual void RunningMovement(float deltaTime)
        {
            var movementDirection = _myController.PollAxisLeft();
            var controllerLookInput = _myController.PollAxisRight();

            if (controllerLookInput.magnitude < 0.01)
            {
                var rawMousePosition = MouseLookUp.Instance.GetMousePosition();
                HeroAvatar.transform.LookAt(new Vector3(rawMousePosition.x, HeroAvatar.transform.position.y, rawMousePosition.z));
            } else
            {
                HeroAvatar.transform.LookAt(new Vector3(HeroAvatar.transform.position.x + controllerLookInput.x, 
                    HeroAvatar.transform.position.y, HeroAvatar.transform.position.z + controllerLookInput.z));
            }
            
            HeroAvatar.Move(movementDirection * ControlledActor.Speed * deltaTime);

            if (_myController.PollButton(Buttons.ButtonScheme.Dash))
            {
                ControlledActor.GoToState(new StateDashing(ControlledActor, movementDirection.normalized));
            }
            else if (_myController.PollButton(Buttons.ButtonScheme.Fire_2))
            {
                ControlledActor.FireGunByIndex(1);
            }
            else if (_myController.PollButton(Buttons.ButtonScheme.Fire_1))
            {
                ControlledActor.FireGunByIndex(0);
            }
        }

        public StateRunning(ActorComponent hero) : base(hero)
        {
            _myController = ControlledActor.gameObject.GetComponent<InputController>();
        }
    }
}
