using fi.tamk.hellgame.interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using fi.tamk.hellgame.character;
using System;
using fi.tamk.hellgame.states;

public class StatePaused : StateAbstract
{
    public override InputStates StateId
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
            case InputStates.Paused:
                return TransitionType.Illegal;
            default:
                return TransitionType.LegalTwoway;
        }
    }

    public StatePaused(ActorComponent controlledHero) : base(controlledHero)
    {
        DamageMultiplier = 0f;
    }
}
