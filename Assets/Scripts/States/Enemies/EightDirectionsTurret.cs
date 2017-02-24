using System.Collections;
using System.Collections.Generic;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using UnityEngine;
using fi.tamk.hellgame.utils;
using System.Linq;

namespace fi.tamk.hellgame.states
{
    public class EightDirectionsTurret : AimingEnemy
    {
        private List<Vector3> _possibleRotations = new List<Vector3>();

        public EightDirectionsTurret(ActorComponent hc) : base(hc)
        {
            var degrees = 360 / 8;
            for (int i = 0; i < 8; i++)
            {
                _possibleRotations.Add(Quaternion.Euler(new Vector3(0, degrees * i, 0)) * Vector3.forward);
            }
        }

        protected override void FaceTargetBehaviour(float deltaTime)
        {
            if (TargetTransform != null)
            {
                Vector3 targetRotation = Vector3.zero;
                Vector3 directionToPlayer = new Vector3(TargetTransform.position.x, ControlledActor.transform.position.y,
                    TargetTransform.position.z) - ControlledActor.transform.position;

                ControlledActor.transform.forward = _possibleRotations.OrderBy(x => Vector3.Angle(directionToPlayer, x)).First();
            }

            RetryTimer += deltaTime;
            if (RetryTimer > RetryTimeout)
            {
                RetryTimer = 0f;
                TargetTransform = ServiceLocator.Instance.GetNearestPlayer(ControlledActor.transform.position);
            }
        }

        public override void HandleInput(float deltaTime)
        {
            FaceTargetBehaviour(deltaTime);
            if (TargetTransform == null) return;
            if ((TargetTransform.position - ControlledActor.transform.position).magnitude > ControlledActor.ActorNumericData.ActorFloatData[1]) {
                HeroAvatar.Move(ControlledActor.transform.forward * deltaTime * ControlledActor.ActorNumericData.ActorFloatData[0]);
                ControlledActor.FireGunByIndex(0);
            }
        }

        public override InputStates StateId
        {
            get
            {
                return InputStates.EightDirectionsTurret;
            }
        }
    }
}
