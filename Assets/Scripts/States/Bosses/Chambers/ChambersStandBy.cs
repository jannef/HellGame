using UnityEngine;
using System.Collections;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;

namespace fi.tamk.hellgame.states
{
    public class ChambersStandBy : ChambersBase
    {
        public ChambersStandBy(ActorComponent controlledHero, ChambersBase clonedState = null)
            : base(controlledHero, clonedState)
        {

        }

        protected override void OnHealthChange(float percentage, int currentHp, int maxHp)
        {
            ControlledActor.GoToState(new ChambersPhaseOne(ControlledActor, this));
        }

        public override InputStates StateId
        {
            get { return InputStates.ChambersStandby; }
        }
    }
}
