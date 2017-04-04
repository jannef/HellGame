using fi.tamk.hellgame.character;
using fi.tamk.hellgame.effects;
using UnityEngine;

namespace fi.tamk.hellgame.states
{
    public class CourtyardBasicFirePhase : CourtyardBase
    {
        private readonly float _endHp;

        public CourtyardBasicFirePhase(ActorComponent controlledHero, float endHealthPercentage = .75f, CourtyardBase clonedState = null) : base(controlledHero, clonedState)
        {
            _endHp = clonedState == null ? endHealthPercentage : TransitionPercentage;
            ControlledActor.ActorNumericData.ReferenceCache[0] = ControlledActor.gameObject.GetComponentInChildren<AngryShakeEffect>();
        }

        public override void HandleInput(float deltaTime)
        {
            base.HandleInput(deltaTime);

            MoveGun();
            ControlledActor.FireGunByIndex(0);
            ControlledActor.FireGunByIndex(1);
            ControlledActor.FireGunByIndex(3);
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
