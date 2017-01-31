using fi.tamk.hellgame.states;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using System;

namespace fi.tamk.hellgame.states
{
    public class StateObstacle : StateAbstract
    {
        public StateObstacle(ActorComponent controlledHero) : base(controlledHero)
        {
        }

        public override InputStates StateID
        {
            get
            {
                return InputStates.Obstacle;
            }
        }

        protected override void CheckForFalling()
        {
        }

        public override bool TakeDamage(int howMuch)
        {
            var status = base.TakeDamage(howMuch);
            if (status)
            {
                ControllerHealth.FlinchFromHit();
            }
            return status;
        }

        public override void OnEnterState()
        {
            base.OnEnterState();
            HeroAvatar.enabled = false;
            ControlledActor.enabled = false;
        }
    }
}
