using System;
using UnityEngine;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.utils;

namespace fi.tamk.hellgame.states
{
    public class ChambersPhaseOne : ChambersBase
    {
        private float _laserCycle;

        public ChambersPhaseOne(ActorComponent controlledHero, ChambersBase clonedState = null) : base(controlledHero, clonedState)
        {
            _laserCycle = NumericData.ActorFloatData[(int) FloatDataLabels.PhaseOneLaserCooldown] +
                          NumericData.ActorFloatData[(int) FloatDataLabels.PhaseOneLaserBurstDuration];
        }

        public override InputStates StateId
        {
            get { return InputStates.ChambersPhaseOne; }
        }

        public override void HandleInput(float deltaTime)
        {
            base.HandleInput(deltaTime);
            if (PlayerTransform != null) NavigationAgent.SetDestination(PlayerTransform.position);
            if (StateTime > _laserCycle)
            {
                StateTime = 0f;
                DefaultLaserBurst();
            }
        }

        public override void OnEnterState()
        {
            base.OnEnterState();
            DefaultLaserBurst();
        }

        protected override void OnHealthChange(float percentage, int currentHp, int maxHp)
        {
            base.OnHealthChange(percentage, currentHp, maxHp);
            if (currentHp <= 1) ControlledActor.GoToState(new ChambersIntermission(ControlledActor, this));
        }
    }
}
