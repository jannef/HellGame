using fi.tamk.hellgame.interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using fi.tamk.hellgame.character;
using System;
using fi.tamk.hellgame.states;

public class StatePaused : StateAbstract
{
    public override InputStates StateID
    {
        get
        {
            return InputStates.Paused;
        }
    }
    public override TransitionType CheckTransitionLegality(InputStates toWhichState)
    {
        switch (toWhichState)
        {
            case InputStates.Dashing:
            case InputStates.Paused:
            case InputStates.Dead:
                return TransitionType.LegalTwoway;
            default:
                return TransitionType.Illegal;
        }
    }

    public StatePaused(ActorComponent controlledHero) : base(controlledHero)
    {

    }
}
