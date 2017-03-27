using System.Collections;
using System.Collections.Generic;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using UnityEngine;

namespace fi.tamk.hellgame.states
{
    public class CourtyardBasicFirePhase : CourtyardBase
    {
        private float _endHp;

        public CourtyardBasicFirePhase(ActorComponent controlledHero, float endHealthPercentage = .75f, CourtyardBase clonedState = null) : base(controlledHero, clonedState)
        {
            _endHp = endHealthPercentage;
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
            if (percentage < _endHp) Debug.Log("Phase ended!");
        }
    }
}
