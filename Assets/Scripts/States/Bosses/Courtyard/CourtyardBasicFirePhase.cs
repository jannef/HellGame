using fi.tamk.hellgame.character;
using UnityEngine;

namespace fi.tamk.hellgame.states
{
    public class CourtyardBasicFirePhase : CourtyardBase
    {
        private readonly float _endHp;

        public CourtyardBasicFirePhase(ActorComponent controlledHero, float endHealthPercentage = .75f, CourtyardBase clonedState = null) : base(controlledHero, clonedState)
        {
            if (clonedState == null)
            {
                _endHp = endHealthPercentage;
            }
            else
            {
                _endHp = TransitionPercentage;
            }
        }

        public override void HandleInput(float deltaTime)
        {
            base.HandleInput(deltaTime);

            MoveGun();
            ControlledActor.FireGunByIndex(0);
            ControlledActor.FireGunByIndex(1);
        }

        protected override void OnHealthChange(float percentage, int currentHp, int maxHp)
        {
            if (percentage < _endHp)
            {
                TransitionPercentage = _endHp - 0.1f;
                var intermission = new CourtyardIntermission(ControlledActor, new CourtyardFloodPhase(ControlledActor, this), this);
                ControlledActor.GoToState(intermission);
            }
        }
    }
}
