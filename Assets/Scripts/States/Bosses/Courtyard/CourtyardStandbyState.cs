using UnityEngine;
using System.Collections;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.effects;
using fi.tamk.hellgame.interfaces;

namespace fi.tamk.hellgame.states
{
    public class CourtyardStandbyState : CourtyardBase
    {
        private bool _transferInProgress = false;

        public CourtyardStandbyState(ActorComponent controlledHero, CourtyardBase clonedState = null) : base(controlledHero, clonedState)
        {
        }

        protected override void OnHealthChange(float percentage, int currentHp, int maxHp)
        {
            if (_transferInProgress) return;

            _transferInProgress = true;
            TransitionPercentage = 0.75f;
            ControlledActor.GoToState(new CourtyardBasicFirePhase(ControlledActor, 0.75f, this));
        }

        public override void OnExitState()
        {
            
            ControlledActor.gameObject.GetComponentInChildren<SphereShellController>().StartAnimations();
        }
    }
}
