using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using UnityEngine;
using fi.tamk.hellgame.world;
using fi.tamk.hellgame.utils;
using fi.tamk.hellgame.dataholders;

namespace fi.tamk.hellgame.states
{
    class WallBoss : WallBossAbstract
    {
        private enum AttackState
        {
            LaserHop, BulletHell, BulletHellSecond
        }


        private PassiveTurret _leftEye;
        private PassiveTurret _rightEye;
        private event Action BackFromMoveState;
        private AttackState _currentAttackState = AttackState.LaserHop;

        private WallBossMovement _SlowMove;
        private WallBossMovement _QuickMove;
        private WallBossBulletHellPhaseStats _firstBulletHellPhase;
        private WallBossLaserAttackStats _firstLaserAttack;
        private WallBossBulletHellPhaseStats _secondBulletHellPhase;
        private WallBossLaserAttackStats _secondLaserAttack;
        private WallBossBulletHellPhaseStats _thirdBulletHellPhase;
        private WallBossLaserAttackStats _thirdLaserAttack;
        private WallBossBulletHellPhaseStats _firstSecondBulletHellPhase;
        private WallBossBulletHellPhaseStats _secondSecondBulletHellPhase;
        private WallBossBulletHellPhaseStats _thirdSecondBulletHellPhase;

        private WallBossLaserAttackStats _currentLaserAttack;
        private WallBossBulletHellPhaseStats _currentBulletHellStats;
        private WallBossBulletHellPhaseStats _secondBulletHellStats;

        public WallBoss(ActorComponent controlledHero) : base(controlledHero)
        {
            Initialize();
        }

        public WallBoss(ActorComponent controlledHero, WallBossAbstractValues values) : base(controlledHero, values)
        {
            Initialize();
        }

        private void Initialize()
        {
            var externalObjects = ControlledActor.gameObject.GetComponent<BossExternalObjects>();
            _leftEye = externalObjects.ExistingGameObjects[1].GetComponent<PassiveTurret>();
            _rightEye = externalObjects.ExistingGameObjects[2].GetComponent<PassiveTurret>();
            _SlowMove = UnityEngine.Object.Instantiate(externalObjects.ScriptableObjects[0]) as WallBossMovement;
            _QuickMove = UnityEngine.Object.Instantiate(externalObjects.ScriptableObjects[1]) as WallBossMovement;
            _firstBulletHellPhase = UnityEngine.Object.Instantiate(externalObjects.ScriptableObjects[3]) as WallBossBulletHellPhaseStats;
            _firstLaserAttack = UnityEngine.Object.Instantiate(externalObjects.ScriptableObjects[4]) as WallBossLaserAttackStats;
            _secondBulletHellPhase = UnityEngine.Object.Instantiate(externalObjects.ScriptableObjects[5]) as WallBossBulletHellPhaseStats;
            _secondLaserAttack = UnityEngine.Object.Instantiate(externalObjects.ScriptableObjects[6]) as WallBossLaserAttackStats;
            _thirdBulletHellPhase = UnityEngine.Object.Instantiate(externalObjects.ScriptableObjects[7]) as WallBossBulletHellPhaseStats;
            _thirdLaserAttack = UnityEngine.Object.Instantiate(externalObjects.ScriptableObjects[8]) as WallBossLaserAttackStats;

            _firstSecondBulletHellPhase = UnityEngine.Object.Instantiate(externalObjects.ScriptableObjects[11]) as WallBossBulletHellPhaseStats;
            _secondSecondBulletHellPhase = UnityEngine.Object.Instantiate(externalObjects.ScriptableObjects[12]) as WallBossBulletHellPhaseStats;
            _thirdSecondBulletHellPhase = UnityEngine.Object.Instantiate(externalObjects.ScriptableObjects[13]) as WallBossBulletHellPhaseStats;

            SetupStats();
        }

        private void SetupStats()
        {
            if (BaseValues.phaseNumber == 0)
            {
                _currentLaserAttack = _firstLaserAttack;
                _currentBulletHellStats = _firstBulletHellPhase;
                _secondBulletHellStats = _firstSecondBulletHellPhase;
            } else if (BaseValues.phaseNumber == 1)
            {
                _currentLaserAttack = _secondLaserAttack;
                _currentBulletHellStats = _secondBulletHellPhase;
                _secondBulletHellStats = _secondSecondBulletHellPhase;
            } else
            {
                _currentLaserAttack = _thirdLaserAttack;
                _currentBulletHellStats = _thirdBulletHellPhase;
                _secondBulletHellStats = _thirdSecondBulletHellPhase;
            }
        }

        

        public override void HandleInput(float deltaTime)
        {
            base.HandleInput(deltaTime);
            Reason();
        }

        private void Reason()
        {
            switch (_currentAttackState)
            {
                case AttackState.BulletHell:
                    BulletHell(_secondBulletHellStats);
                    _currentAttackState = AttackState.BulletHellSecond;
                    break;
                case AttackState.BulletHellSecond:
                    StartLaserAttack();
                    _currentAttackState = AttackState.LaserHop;
                    break;
                case AttackState.LaserHop:
                    BulletHell(_currentBulletHellStats);
                    _currentAttackState = AttackState.BulletHell;
                    break;
            }
        }

        public override void OnResumeState()
        {
            base.OnResumeState();

            if (BackFromMoveState != null) BackFromMoveState.Invoke();
            
        }

        private void BulletHell(WallBossBulletHellPhaseStats stats)
        {
            ControlledActor.GoToState(new WallBossBulletHell(ControlledActor, BaseValues, _leftEye, _rightEye, stats));
            
        }

        private void StartLaserAttack()
        {
            
            BaseValues.currentPositionIndex = DetermineCurrentPositionIndex();
            _leftEye.StopFiring();
            _rightEye.StopFiring();

            if (BaseValues.currentPositionIndex == 1)
            {
                if (UnityEngine.Random.value < 0.5)
                {
                    BaseValues.currentPositionIndex = 0;
                } else
                {
                    BaseValues.currentPositionIndex = 2;
                }

            } else if (BaseValues.currentPositionIndex == 0)
            {
                BaseValues.currentPositionIndex = 2;
            } else
            {
                BaseValues.currentPositionIndex = 0;
            }

            ControlledActor.GoToState(new WallBossMove(ControlledActor, BaseValues, BaseValues.wayPointList.WayPointList[BaseValues.currentPositionIndex].position,
                _QuickMove));

            BackFromMoveState += FirstLaserAttack;
            return;
        }

        private int DetermineCurrentPositionIndex()
        {
            var magnitudeToClosestPoint = Mathf.Infinity;
            int returnValue = 0;

            for (int i = 0; i < BaseValues.wayPointList.WayPointList.Length; i++)
            {
                if ((BaseValues.wayPointList.WayPointList[i].position - ControlledActor.transform.position).sqrMagnitude < magnitudeToClosestPoint)
                {
                    returnValue = i;
                    magnitudeToClosestPoint = (BaseValues.wayPointList.WayPointList[i].position - ControlledActor.transform.position).sqrMagnitude;
                }
            }

            return returnValue;
        }

        private void FirstLaserAttack()
        {
            BackFromMoveState -= FirstLaserAttack;

            ControlledActor.GoToState(new WallBossLaserAttack(ControlledActor, BaseValues, _leftEye, _rightEye, _currentLaserAttack));
        }

        private void StartMove()
        {
            int positionIndex = UnityEngine.Random.Range((int)0, (int)3);

            while (positionIndex == BaseValues.currentPositionIndex)
            {
                positionIndex = UnityEngine.Random.Range((int)0, (int)3);
            }

            BaseValues.currentPositionIndex = positionIndex;
            ControlledActor.GoToState(new WallBossMove(ControlledActor, BaseValues, BaseValues.wayPointList.WayPointList[BaseValues.currentPositionIndex].position,
               _SlowMove));
        }

        public override InputStates StateId
        {
            get
            {
                return InputStates.PatrollingEnemy;
            }
        }
    }
}
