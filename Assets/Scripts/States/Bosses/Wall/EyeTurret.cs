using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.states;
using fi.tamk.hellgame.utils;
using UnityEngine;

namespace fi.tamk.hellgame.states
{
    class EyeTurret : StateAbstract
    {
        protected Transform TargetTransform;
        protected float RetryTimer = 0f;
        protected float RetryTimeout = 0.31f;
        private int weaponIndex = 0;

        public EyeTurret(ActorComponent controlledHero) : base(controlledHero)
        {
            TargetTransform = ServiceLocator.Instance.GetNearestPlayer(ControlledActor.transform.position);
        }

        public override InputStates StateId
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override void HandleInput(float deltaTime)
        {
            base.HandleInput(deltaTime);
            FaceTargetBehaviour(deltaTime);

            if (weaponIndex == -1) return;

            ControlledActor.FireGunByIndex(weaponIndex);
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
