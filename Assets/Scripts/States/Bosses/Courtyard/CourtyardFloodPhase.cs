using fi.tamk.hellgame.character;
using UnityEngine;

namespace fi.tamk.hellgame.states
{
    public class CourtyardFloodPhase : CourtyardBase
    {
        private const float rotationSpeed = 15f;

        public CourtyardFloodPhase(ActorComponent controlledHero, CourtyardBase clonedState = null) : base(controlledHero, clonedState)
        {
        }

        public override void HandleInput(float deltaTime)
        {
            base.HandleInput(deltaTime);

            RotateGun(deltaTime);
            ControlledActor.FireGunByIndex(2);
        }

        private void RotateGun(float deltaTime)
        {
            GunPivot.Rotate(Vector3.up * rotationSpeed * deltaTime);
        }
    }
}
