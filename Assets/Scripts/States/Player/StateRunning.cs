using fi.tamk.hellgame.interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using fi.tamk.hellgame.character;
using System;
using fi.tamk.hellgame.dataholders;
using fi.tamk.hellgame.input;
using tamk.fi.hellgame.character;

namespace fi.tamk.hellgame.states
{
    public class StateRunning : StateAbstract
    {
        protected InputController _myController;
        protected PlayerLimitBreak _myLimitBreak;

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
            var movementSpeedMultiplier = (1 - _myController.PollLeftTrigger() * .55f); 
            var controllerLookInput = _myController.PollAxisRight();

            HeroAvatar.transform.LookAt(new Vector3(HeroAvatar.transform.position.x + controllerLookInput.x,
                HeroAvatar.transform.position.y, HeroAvatar.transform.position.z + controllerLookInput.z));
            
            HeroAvatar.Move(movementDirection * ControlledActor.ActorNumericData.ActorFloatData[(int)ActorDataMap.Speed] * deltaTime * movementSpeedMultiplier);

            if (movementSpeedMultiplier <= 0.95)
            {
                if (_myLimitBreak != null)
                {
                    if (_myLimitBreak.LimitBreakActive)
                    {
                        _myLimitBreak.ActivateLimitBreak();
                        return;
                    }
                }

                ControlledActor.GoToState(new StateCharging(ControlledActor));
            }

            if (_myController.PollButtonDown(Buttons.ButtonScheme.Dash))
            {
                ControlledActor.GoToState(new StateDashing(ControlledActor, movementDirection.normalized, movementSpeedMultiplier));
            }
            else if (_myController.PollButton(Buttons.ButtonScheme.Fire_1) || controllerLookInput.sqrMagnitude > 0.001)
            {
                ControlledActor.FireGunByIndex(0);
            }
        }

        public StateRunning(ActorComponent hero) : base(hero)
        {
            _myController = ControlledActor.gameObject.GetComponent<InputController>();
            _myLimitBreak = ControlledActor.gameObject.GetComponent<PlayerLimitBreak>();
        }
    }
}
