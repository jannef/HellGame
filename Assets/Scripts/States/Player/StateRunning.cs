using fi.tamk.hellgame.interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using fi.tamk.hellgame.character;
using System;
using tamk.fi.hellgame.character;

namespace fi.tamk.hellgame.states
{
    public class StateRunning : StateAbstract
    {
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
            var movementDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            var controllerLookInput = new Vector3(Input.GetAxis("HorizontalRight"), 0, Input.GetAxis("VerticalRight"));

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

            if (Input.GetButtonDown("Jump"))
            {
                ControlledActor.GoToState(new StateDashing(ControlledActor, movementDirection.normalized));
            }
            else if (Input.GetButton("Fire2"))
            {
                ControlledActor.FireGunByIndex(1);
            }
            if (Input.GetButton("Fire1") || controllerLookInput.magnitude > 0.1)
            {
                ControlledActor.FireGunByIndex(0);
            }
        }

        public StateRunning(ActorComponent hero) : base(hero)
        {

        }
    }
}
