using UnityEngine;
using System.Collections;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using System;

namespace fi.tamk.hellgame.states
{
    public class ObstacleState : StateAbstract
    {
        public ObstacleState(ActorComponent controlledHero) : base(controlledHero)
        {

        }

        public override InputStates StateId
        {
            get
            {
                return InputStates.Obstacle;
            }
        }

        public override TransitionType CheckTransitionLegality(InputStates toWhichState)
        {
            return TransitionType.Illegal;
        }
    }
}
