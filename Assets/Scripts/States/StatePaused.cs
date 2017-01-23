using fi.tamk.hellgame.interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using fi.tamk.hellgame.character;
using System;

public class StatePaused : IInputState
{
    public HeroController ControlledCharacter
    {
        get
        {
            return _hero;
        }
    }
    private HeroController _hero;

    public InputStates StateID
    {
        get
        {
            return InputStates.Paused;
        }
    }

    public float StateTimer
    {
        get
        {
            return _stateTime;
        }
    }
    private float _stateTime;

    public TransitionType CheckTransitionLegality(InputStates toWhichState)
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

    public void HandleInput(float deltaTime)
    {
        _stateTime += deltaTime;
    }

    public void OnEnterState()
    {
        Debug.Log("Paused::OnEnterState");
    }

    public void OnExitState()
    {
        Debug.Log("Paused::OnExitState");
    }

    public void OnResumeState()
    {
        Debug.Log("Paused::OnResumeState");
    }

    public void OnSuspendState()
    {
        Debug.Log("Paused::OnSuspendState");
    }

    public StatePaused(HeroController controlledHero)
    {
        _stateTime = 0f;
        _hero = controlledHero;
    }
}
