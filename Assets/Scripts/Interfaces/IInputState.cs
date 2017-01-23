﻿using fi.tamk.hellgame.character;

namespace fi.tamk.hellgame.interfaces
{
    public enum InputStates
    {
        Running     = 0,
        Error       = -1,
        Paused      = 1,
        Dashing     = 2,
        Dead        = 3 
    }

    public enum TransitionType
    {
        Illegal         = 0,
        LegalTwoway     = 1,
        LegalOneway     = 2,
    }

    public interface IInputState
    {
        InputStates StateID { get; }
        float StateTimer { get; }
        HeroController ControlledCharacter { get; }

        void OnEnterState();
        void OnResumeState();
        void OnExitState();
        void OnSuspendState();
        void HandleInput(float deltaTime);

        TransitionType CheckTransitionLegality(InputStates toWhichState);
        string ToString();
    }
}
