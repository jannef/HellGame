using fi.tamk.hellgame.character;

namespace fi.tamk.hellgame.interfaces
{
    public enum InputStates
    {
        Running     = 0,
        Error       = -1,
        Paused      = 1,
        Dashing     = 2,
        Dead        = 3,
        EnemyTurret = 4,
        Invulnerable = 5,
        HomingEnemy = 6,
        Falling = 7,
        AirDeploymentState = 8,
        Obstacle = 9
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
        ActorComponent ControlledActor { get; }

        void OnEnterState();
        void OnResumeState();
        void OnExitState();
        void OnSuspendState();
        bool TakeDamage(int howMuch);
        void HandleInput(float deltaTime);

        TransitionType CheckTransitionLegality(InputStates toWhichState);
        string ToString();
    }
}
