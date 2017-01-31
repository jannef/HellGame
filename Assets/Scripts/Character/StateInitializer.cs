
using System;
using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.states;
using fi.tamk.hellgame.utils;
using UnityEngine;

namespace fi.tamk.hellgame.character
{
    [RequireComponent(typeof(ActorComponent))]
    class StateInitializer : MonoBehaviour
    {
        [SerializeField] protected InputStates InitialState;
        [SerializeField] protected bool RegisterAsPlayer;
        [SerializeField] protected float CameraWeight = 0f;

        protected void Awake()
        {
            if (RegisterAsPlayer) ServiceLocator.Instance.RegisterPlayer(transform);
            if (CameraWeight > 0f) ServiceLocator.Instance.MainCameraScript.AddInterest(new CameraInterest(transform, CameraWeight));

            var hc = GetComponent<ActorComponent>();
            IInputState tmp = null;
            switch (InitialState)
            {
                case InputStates.Running:
                    tmp = new StateRunning(hc);
                    break;
                case InputStates.Error:
                    break;
                case InputStates.Paused:
                    tmp = new StatePaused(hc);
                    break;
                case InputStates.Dashing:
                    tmp = new StateDashing(hc, Vector3.forward);
                    break;
                case InputStates.HomingEnemy:
                    tmp = new HomingEnemyState(hc);
                    break;
                case InputStates.Invulnerable:
                    tmp = new StateInvulnerable(hc);
                    break;
                case InputStates.Dead:
                    break;
                case InputStates.EnemyTurret:
                    tmp = new EnemyTurret(hc);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            hc.InitializeStateMachine(tmp);
        }

        protected virtual void OnDestroy()
        {
            if (!ServiceLocator.Quitting) ServiceLocator.Instance.UnregisterPlayer(transform);
            if (!ServiceLocator.Quitting) ServiceLocator.Instance.MainCameraScript.RemoveInterest(transform);
        }
    }
}
