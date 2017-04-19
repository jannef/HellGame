using UnityEngine;
using System.Collections;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.world;

namespace fi.tamk.hellgame.states
{
    public class ChambersStandBy : ChambersBase
    {
        private bool _triggered = false;

        public ChambersStandBy(ActorComponent controlledHero, ChambersBase clonedState = null)
            : base(controlledHero, clonedState)
        {

        }

        protected override void OnHealthChange(float percentage, int currentHp, int maxHp)
        {
            if (_triggered) return;
            _triggered = true;

            ControlledActor.StartCoroutine(TransitionToPhaseOne(
                NumericData.ActorFloatData[(int)FloatDataLabels.CogBurstDuration],
                (int)NumericData.ActorFloatData[(int)FloatDataLabels.CogBursts]));
        }

        public override InputStates StateId
        {
            get { return InputStates.ChambersStandby; }
        }

        private IEnumerator TransitionToPhaseOne(float duration, int bursts = 3)
        {
            if (bursts < 1) bursts = 1;

            var dur = duration / bursts;

            for (var i = 0; i < bursts; i++)
            {
                CogGun.Fire();
                var timer = 0f;
                while (timer < dur)
                {
                    timer += WorldStateMachine.Instance.DeltaTime;
                    yield return null;
                }
            }

            ControlledActor.GoToState(new ChambersPhaseOne(ControlledActor, this));
        }
    }
}
