using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.utils;
using UnityEngine;

namespace fi.tamk.hellgame.states
{
    class StateFalling : StateAbstract
    {

        public StateFalling(ActorComponent controlledHero) : base(controlledHero)
        {
            DamageMultiplier = 1f;
        }

        public override TransitionType CheckTransitionLegality(InputStates toWhichState)
        {
            switch (toWhichState)
            {
                case InputStates.Paused:
                    return TransitionType.LegalTwoway;
                case InputStates.Dead:
                case InputStates.Running:
                    return TransitionType.LegalOneway;
                default:
                    return TransitionType.Illegal;
            }
        }

        protected override void CheckForFalling()
        {
        }

        public override InputStates StateId
        {
            get { return InputStates.Falling; }
        }

        public override void Teleport(Vector3 targetLocation)
        {
            base.Teleport(targetLocation);
            while (ControlledActor.ToPreviousState());
        }

        public override void HandleInput(float deltaTime)
        {
            base.HandleInput(deltaTime);
            HeroAvatar.Move(Vector3.down * 25f * deltaTime);

            if (StateTime > Constants.FallingDeathLenght)
            {
                HealthComponent hc = Pool.Instance.GetHealthComponent(ControlledActor.gameObject);
                if (hc == null) return;
                hc.TakeDisplacingDamage(1);
            }
        }
    }
}
