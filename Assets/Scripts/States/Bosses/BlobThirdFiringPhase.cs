using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.states
{
    public class BlobThirdFiringPhase : AimingEnemy {
        private float singleBeatLength = 1.4f;
        private int tempo = 0;

        public BlobThirdFiringPhase(ActorComponent hc) : base(hc)
        {
        }

        public override InputStates StateId
        {
            get
            {
                return InputStates.BlobThird;
            }
        }

        public override bool RequestStateChange(InputStates requestedState)
        {
            if (requestedState == InputStates.Paused)
            {
                ControlledActor.GoToState(new StatePaused(ControlledActor));
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
                    _targetTransform.position - ControlledActor.transform.position,
                    ControlledActor.DashSpeed * Time.deltaTime, 0.0f);

                switch (tempo)
                {
                    case 3:
                        ControlledActor.FireGunByIndex(2);
                        break;
                    case 4:
                        ControlledActor.FireGunByIndex(1);
                        break;
                    default:
                        ControlledActor.FireGunByIndex(0);
                        break;
                }

                if (StateTime >= singleBeatLength)
                {
                    StateTime = 0;
                    if (tempo < 5)
                    {
                        tempo++;
                    } else
                    {
                        tempo = 0;
                    }
                    
                }
            }

            _retryTimer += deltaTime;
            if (_retryTimer > RetryTimeout)
            {
                _retryTimer = 0f;
                _targetTransform = ServiceLocator.Instance.GetNearestPlayer(ControlledActor.transform.position);
            }

        }
    }
}
