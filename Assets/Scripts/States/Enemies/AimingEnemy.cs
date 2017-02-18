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
        protected Transform TargetTransform;
        protected float RetryTimer = 0f;
        protected float RetryTimeout = 0.3f;

        public override InputStates StateId
        {
            get
            {
                return InputStates.AimingEnemy;
            }
        }

        public AimingEnemy(ActorComponent hc) : base(hc)
        {
            TargetTransform = ServiceLocator.Instance.GetNearestPlayer(ControlledActor.transform.position);
        }

        public override void HandleInput(float deltaTime)
        {
            base.HandleInput(deltaTime);
            FaceTargetBehaviour(deltaTime);
            ControlledActor.FireGunByIndex(0);
        }

        protected virtual void FaceTargetBehaviour(float deltaTime)
        {
            if (TargetTransform != null)
            {
                ControlledActor.transform.forward = Vector3.RotateTowards(ControlledActor.transform.forward,
                    new Vector3(TargetTransform.position.x, ControlledActor.transform.position.y,
                    TargetTransform.position.z) - ControlledActor.transform.position,
                    ControlledActor.ActorNumericData.ActorFloatData[2] * deltaTime, 0.0f);
            }

            RetryTimer += deltaTime;
            if (RetryTimer > RetryTimeout)
            {
                RetryTimer = 0f;
                TargetTransform = ServiceLocator.Instance.GetNearestPlayer(ControlledActor.transform.position);
            }
        }
    }
}
