using System.Collections;
using System.Collections.Generic;
using fi.tamk.hellgame.interfaces;
using UnityEngine;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.dataholders;
using fi.tamk.hellgame.input;
using tamk.fi.hellgame.character;

namespace fi.tamk.hellgame.states
{

    public class StateCharging : StateRunning
    {
        public override InputStates StateId
        {
            get
            {
                return InputStates.Charging;
            }
        }

        public StateCharging(ActorComponent hero) : base(hero)
        {
            _myController = ControlledActor.gameObject.GetComponent<InputController>();
        }

        public override void OnEnterState()
        {
            base.OnEnterState();
            ControlledActor.FireGunByIndex(1);
        }

        public override void OnExitState()
        {
            base.OnExitState();
            ControlledActor.FireGunByIndex(1);
        }

        protected override void RunningMovement(float deltaTime)
        {
            var movementDirection = _myController.PollAxisLeft();
            var movementSpeedMultiplier = (1 - _myController.PollLeftTrigger() * .55f);
            var controllerLookInput = _myController.PollAxisRight();

            if (controllerLookInput.magnitude < 0.01)
            {
                var rawMousePosition = MouseLookUp.Instance.GetMousePosition();
                HeroAvatar.transform.LookAt(new Vector3(rawMousePosition.x, HeroAvatar.transform.position.y, rawMousePosition.z));
            }
            else
            {
                HeroAvatar.transform.LookAt(new Vector3(HeroAvatar.transform.position.x + controllerLookInput.x,
                    HeroAvatar.transform.position.y, HeroAvatar.transform.position.z + controllerLookInput.z));
            }

            HeroAvatar.Move(movementDirection * ControlledActor.ActorNumericData.ActorFloatData[(int)ActorDataMap.Speed] * deltaTime * movementSpeedMultiplier);

            if (movementSpeedMultiplier >= 0.98)
            {
                ControlledActor.ToPreviousState();
            }

        }

    }
}
