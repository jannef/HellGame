﻿using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.states
{
    public class BlobFirstFiringState : AimingEnemy {
        private float singleBeatLength = 1.6f;
        private int tempo = 0;

        public BlobFirstFiringState(ActorComponent hc) : base(hc)
        {
        }

        public override InputStates StateId
        {
            get
            {
                return InputStates.BlobSecond;
            }
        }

        public override bool RequestStateChange(InputStates requestedState)
        {
            if (requestedState == InputStates.BlobResting)
            {
                ControlledActor.GoToState(new BlobRestPhase(ControlledActor));
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

                switch (tempo)
                {
                    case 3:
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
