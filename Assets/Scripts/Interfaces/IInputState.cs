using fi.tamk.hellgame.character;
using UnityEngine;

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
        Obstacle = 9,
        AimingEnemy = 10,
        PickUp = 11,
        PickUpFollowing = 12,
        Flying = 15,
        Charging = 19,
        SlimeJumping = 20,
        JumpRepeat = 21,
        SlimeHuntJumping = 22,
        MobRoomZero = 23,
        EightDirectionsTurret = 24,
        ShootingSkirmisher = 25,
        PatrollingEnemy = 26,
        SlimeMob = 27,
        WallBoss = 28,
        WallBossTransition = 29,
        CourtyardBasicFire = 30,
        CourtYardBulletFlood = 31,
        TutorialRoom        = 32,
        CourtyardStandby = 33,
        ChambersStandby = 34,
        ChambersPhaseOne = 35,

    }

    public enum TransitionType
    {
        Illegal         = 0,
        LegalTwoway     = 1,
        LegalOneway     = 2,
    }

    public interface IInputState
    {
        InputStates StateId { get; }
        float StateTimer { get; }
        ActorComponent ControlledActor { get; }

        void OnEnterState();
        void OnResumeState();
        void OnExitState();
        void OnSuspendState();
        bool TakeDamage(int howMuch, ref int health, ref bool flinch);
        void HandleInput(float deltaTime);

        /// <summary>
        /// Requests the state to transition to a given state.
        /// </summary>
        /// <param name="requestedState">State into which a transition is requested.</param>
        /// <returns>True if this request is accepted.</returns>
        bool RequestStateChange(InputStates requestedState);

        TransitionType CheckTransitionLegality(InputStates toWhichState);
        string ToString();
    }
}
