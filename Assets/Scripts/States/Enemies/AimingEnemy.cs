using fi.tamk.hellgame.states;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using fi.tamk.hellgame.interfaces;
using System;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.utils;

namespace fi.tamk.hellgame.states
{
    public class AimingEnemy : StateAbstract
    {
        private Transform _targetTransform;
        private float _retryTimer = 0;
        private float RetryTimeout = 0.3f;

        public override InputStates StateId
        {
            get
            {
                return InputStates.AimingEnemy;
            }
        }

        public AimingEnemy(ActorComponent hc) : base(hc)
        {
            _targetTransform = ServiceLocator.Instance.GetNearestPlayer(ControlledActor.transform.position);
        }

        public override void HandleInput(float deltaTime)
        {
            base.HandleInput(deltaTime);

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
