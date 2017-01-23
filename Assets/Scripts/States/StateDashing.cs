using fi.tamk.hellgame.interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using fi.tamk.hellgame.character;
using System;

public class StateDashing : IInputState
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
            return InputStates.Dashing;
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
            case InputStates.Paused:
                return TransitionType.LegalTwoway;
            case InputStates.Dead:
            case InputStates.Running:
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
        Debug.Log("Dashing::OnEnterState");
    }

    public void OnExitState()
    {
        Debug.Log("Dashing::OnExitState");
    }

    public void OnResumeState()
    {
        Debug.Log("Dashing::OnResumeState");
    }

    public void OnSuspendState()
    {
        Debug.Log("Dashing::OnSuspendState");
    }

    public StateDashing(HeroController controlledHero)
    {
        _stateTime = 0f;
        _hero = controlledHero;
    }
}
