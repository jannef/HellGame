using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.states
{
    public class BlobRestPhase : AimingEnemy
    {
        private float singleBeatLength = 1.4f;
        private int tempo = 0;

        public BlobRestPhase(ActorComponent hc) : base(hc)
        {
        }

        public override InputStates StateId
        {
            get
            {
                return InputStates.BlobResting;
            }
        }

        public override bool RequestStateChange(InputStates requestedState)
        {
            if (requestedState == InputStates.BlobThird)
            {
                ControlledActor.GoToState(new BlobThirdFiringPhase(ControlledActor));
                return true;
            }

            return false;
        }

        public override void HandleInput(float deltaTime)
        {
            StateTime += Time.deltaTime;

            if (_targetTransform != null)
            {
                // TODO: Rotating speed is now determined by dashingSpeed
                ControlledActor.transform.forward = Vector3.RotateTowards(ControlledActor.transform.forward,
                    new Vector3(_targetTransform.position.x, ControlledActor.transform.position.y,
                    _targetTransform.position.z) - ControlledActor.transform.position,
                    ControlledActor.DashSpeed * Time.deltaTime, 0.0f);

                _retryTimer += deltaTime;
                if (_retryTimer > RetryTimeout)
                {
                    _retryTimer = 0f;
                    _targetTransform = ServiceLocator.Instance.GetNearestPlayer(ControlledActor.transform.position);
                }

            }
        }
    }
}
