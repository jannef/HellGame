using fi.tamk.hellgame.character;
using UnityEngine;

namespace fi.tamk.hellgame.states
{
    public class CourtyardFloodPhase : CourtyardBase
    {
        private float _rotationSpeed;

        public CourtyardFloodPhase(ActorComponent controlledHero, CourtyardBase clonedState = null) : base(controlledHero, clonedState)
        {
            _rotationSpeed = controlledHero.ActorNumericData.ActorFloatData[(int) ExternalFloats.AutoRotationSpeed];
        }

        public override void HandleInput(float deltaTime)
        {
            base.HandleInput(deltaTime);

            RotateGun(deltaTime);
            ControlledActor.FireGunByIndex(2);
        }

        private void RotateGun(float deltaTime)
        {
            GunPivot.Rotate(Vector3.up * _rotationSpeed * deltaTime);
        }
    }
}
