using fi.tamk.hellgame.interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using fi.tamk.hellgame.character;
using System;

public class StateRunning : IInputState
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
            return InputStates.Running;
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
        switch(toWhichState)
        {
            case InputStates.Dashing:
            case InputStates.Paused:
                return TransitionType.LegalTwoway;
            case InputStates.Dead:
                return TransitionType.LegalOneway;
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
        Debug.Log("Running::OnEnterState");
    }

    public void OnExitState()
    {
        Debug.Log("Running::OnExitState");
    }

    public void OnResumeState()
    {
        Debug.Log("Running::OnResumeState");
    }

    public void OnSuspendState()
    {
        Debug.Log("Running::OnSuspendState");
    }

    public StateRunning(HeroController controlledHero)
    {
        _stateTime = 0f;
        _hero = controlledHero;
    }
}
