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
        public override InputStates StateID
        {
            get { return InputStates.Running; }
        }

        public override TransitionType CheckTransitionLegality(InputStates toWhichState)
        {
            switch (toWhichState)
            {
                case InputStates.Dashing:
                case InputStates.Paused:
                    return TransitionType.LegalTwoway;
                case InputStates.Dead:
                    return TransitionType.LegalOneway;
                default:
                    return TransitionType.Illegal;
            }
        }

        public override void HandleInput(float deltaTime)
        {
            _stateTime += deltaTime;

            var movementDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            
            var rawMousePosition = MouseLookUp.Instance.GetMousePosition();
            HeroAvatar.transform.LookAt(new Vector3(rawMousePosition.x, HeroAvatar.transform.position.y, rawMousePosition.z));
            HeroAvatar.Move(movementDirection * HeroStats.Speed * deltaTime);

            if (Input.GetButtonDown("Jump"))
            {
                ControlledCharacter.GoToState(new StateDashing(ControlledCharacter, movementDirection.normalized));
            } else if (Input.GetButton("Fire1"))
            {
                ControlledCharacter.FireGuns();
            }
        }

        public StateRunning(HeroController hero) : base(hero)
        {

        }
    }
}
