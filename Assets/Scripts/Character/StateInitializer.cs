
using System;
using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.states;
using fi.tamk.hellgame.utils;
using fi.tamk.hellgame.world;
using UnityEngine;

namespace fi.tamk.hellgame.character
{
    [RequireComponent(typeof(ActorComponent))]
    public class StateInitializer : MonoBehaviour
    {
        [SerializeField] protected InputStates InitialState;
        [SerializeField] protected bool RegisterAsPlayer;
        [SerializeField] protected float CameraWeight = 0f;

        protected virtual void Awake()
        {
            if (RegisterAsPlayer)
            {
                ServiceLocator.Instance.RegisterPlayer(gameObject);
            }
            if (CameraWeight > 0f) ServiceLocator.Instance.MainCameraScript.AddInterest(new CameraInterest(transform, CameraWeight));
            InitializeState();
        }


        protected virtual void InitializeState()
        {
            var hc = GetComponent<ActorComponent>();
            IInputState tmp;
            StateFromStateId(InitialState, out tmp, hc);

            hc.InitializeStateMachine(tmp);
        }

        public static void StateFromStateId(InputStates stateID, out IInputState outPutState, ActorComponent hc)
        {
            switch (stateID)
            {
                case InputStates.Running:
                    outPutState = new StateRunning(hc);
                    break;
                case InputStates.Error:
                    outPutState = null;
                    break;
                case InputStates.HomingEnemy:
                    outPutState = new HomingEnemyState(hc);
                    break;
                case InputStates.Dead:
                    outPutState = null;
                    break;
                case InputStates.EnemyTurret:
                    outPutState = new EnemyTurret(hc);
                    break;
                case InputStates.AimingEnemy:
                    outPutState = new AimingEnemy(hc);
                    break;
                case InputStates.JumpRepeat:
                    outPutState = new JumpRepeatState(hc);
                    break;
                case InputStates.MobRoomZero:
                    outPutState = new MobRoomZero(hc);
                    break;
                case InputStates.EightDirectionsTurret:
                    outPutState = new EightDirectionsTurret(hc);
                    break;
                case InputStates.ShootingSkirmisher:
                    outPutState = new ShootingSkirmisherFollowingState(hc);
                    break;
                case InputStates.PatrollingEnemy:
                    outPutState = new PartollingEnemy(hc);
                    break;
                case InputStates.SlimeMob:
                    outPutState = new BlobMobVersion(hc);
                    break;
                case InputStates.WallBoss:
                    outPutState = new WallBoss(hc);
                    break;
                case InputStates.CourtyardBasicFire:
                    outPutState = new CourtyardBasicFirePhase(hc);
                    break;
                case InputStates.CourtYardBulletFlood:
                    outPutState = new CourtyardFloodPhase(hc);
                    break;
                case InputStates.Obstacle:
                    outPutState = new ObstacleState(hc);
                    break;
                case InputStates.TutorialRoom:
                    outPutState = new TutorialRoom(hc);
                    break;
                default:
                    outPutState = null;
                    throw new Exception("StateInitializer.StateFromStateId : " +
                                        "fi.tamk.hellgame.character: Enumerated value is out of range." +
                                        "Use fi.tamk.hellgame.interfaces enumeration type!");
            }
        }

        protected virtual void OnDestroy()
        {
            if (SceneLoadLock.SceneChangeInProgress) return;

            if (!ServiceLocator.Quitting) ServiceLocator.Instance.UnregisterPlayer(gameObject);
            if (!ServiceLocator.Quitting) ServiceLocator.Instance.MainCameraScript.RemoveInterest(transform);
        }
    }
}
