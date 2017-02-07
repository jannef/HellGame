using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.states
{
    public class BlobSecondFiringPhase : AimingEnemy {

        public BlobSecondFiringPhase(ActorComponent hc) : base(hc)
        {
        }

        public override InputStates StateId
        {
            get
            {
                return InputStates.BlobSecond;
            }
        }

        public override void HandleInput(float deltaTime)
        {

            if (_targetTransform != null)
            {
                // TODO: Rotating speed is now determined by dashingSpeed
                ControlledActor.transform.forward = Vector3.RotateTowards(ControlledActor.transform.forward,
                    _targetTransform.position - ControlledActor.transform.position,
                    ControlledActor.DashSpeed * Time.deltaTime, 0.0f);
                ControlledActor.FireGuns();
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
