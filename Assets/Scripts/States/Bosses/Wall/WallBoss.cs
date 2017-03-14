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
    class WallBoss : StateAbstract
    {
        private enum AttackState
        {
            LaserHop, BulletHell
        }


        private PassiveTurret _leftEye;
        private PassiveTurret _rightEye;
        private AirSpawnerWithSetSpawnPoints _mobSpawner;
        private PatrolWayPoint _wayPoints;
        private int currentPositionIndex = 0;
        private event Action BackFromMoveState;
        private event Action StopFiringLasers;
        private delegate void  OnTickDelegate(float deltaTime);
        private event OnTickDelegate OnTickEvent;
        private AttackState _currentAttackState = AttackState.LaserHop;
        private int _phase = 0;

        private WallBossMovement _SlowMove;
        private WallBossMovement _QuickMove;
        private WallBossMovement _FirstLaserMove;
        private WallBossBulletHellPhaseStats _firstBulletHellPhase;
        private WallBossLaserAttackStats _firstLaserAttack;
        private WallBossBulletHellPhaseStats _secondBulletHellPhase;
        private WallBossLaserAttackStats _secondLaserAttack;
        private WallBossBulletHellPhaseStats _thirdBulletHellPhase;
        private WallBossLaserAttackStats _thirdLaserAttack;

        private WallBossLaserAttackStats _currentLaserAttack;
        private WallBossBulletHellPhaseStats _currentBulletHellStats;

        private int _phaseNumber = 0;

        public WallBoss(ActorComponent controlledHero) : base(controlledHero)
        {
            var hc = ControlledActor.GetComponent<HealthComponent>();
            hc.HealthChangeEvent += OnBossHealthChange;
            var externalObjects = ControlledActor.gameObject.GetComponent<BossExternalObjects>();
            _mobSpawner = externalObjects.ExistingGameObjects[0].GetComponent<AirSpawnerWithSetSpawnPoints>();
            _leftEye = externalObjects.ExistingGameObjects[1].GetComponent<PassiveTurret>();
            _rightEye = externalObjects.ExistingGameObjects[2].GetComponent<PassiveTurret>();
            _wayPoints = externalObjects.ExistingGameObjects[3].GetComponent<PatrolWayPoint>();
            _SlowMove = UnityEngine.Object.Instantiate(externalObjects.ScriptableObjects[0]) as WallBossMovement;
            _QuickMove = UnityEngine.Object.Instantiate(externalObjects.ScriptableObjects[1]) as WallBossMovement;
            _FirstLaserMove = UnityEngine.Object.Instantiate(externalObjects.ScriptableObjects[2]) as WallBossMovement;
            _firstBulletHellPhase = UnityEngine.Object.Instantiate(externalObjects.ScriptableObjects[3]) as WallBossBulletHellPhaseStats;
            _firstLaserAttack = UnityEngine.Object.Instantiate(externalObjects.ScriptableObjects[4]) as WallBossLaserAttackStats;
            _secondBulletHellPhase = UnityEngine.Object.Instantiate(externalObjects.ScriptableObjects[5]) as WallBossBulletHellPhaseStats;
            _secondLaserAttack = UnityEngine.Object.Instantiate(externalObjects.ScriptableObjects[6]) as WallBossLaserAttackStats;
            _thirdBulletHellPhase = UnityEngine.Object.Instantiate(externalObjects.ScriptableObjects[7]) as WallBossBulletHellPhaseStats;
            _thirdLaserAttack = UnityEngine.Object.Instantiate(externalObjects.ScriptableObjects[8]) as WallBossLaserAttackStats;

            _currentLaserAttack = _firstLaserAttack;
            _currentBulletHellStats = _firstBulletHellPhase;

        }

        private void OnBossHealthChange(float percentage, int health, int maxHealth)
        {
            if (_phase <= 0 && percentage < 0.66f)
            {
                _currentLaserAttack = _secondLaserAttack;
                _currentBulletHellStats = _secondBulletHellPhase;
                _phase++;
            }
            else if (_phase <= 1 && percentage < 0.33f)
            {
                _currentLaserAttack = _thirdLaserAttack;
                _currentBulletHellStats = _thirdBulletHellPhase;
                _phase++;

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
                    StartLaserAttack();
                    _currentAttackState = AttackState.LaserHop;
                    break;
                case AttackState.LaserHop:
                    BulletHell();
                    _currentAttackState = AttackState.BulletHell;
                    break;
            }
        }

        public override void OnResumeState()
        {
            base.OnResumeState();

            if (BackFromMoveState != null) BackFromMoveState.Invoke();
            
        }

        private void BulletHell()
        {
            ControlledActor.GoToState(new WallBossBulletHell(ControlledActor, _leftEye, _rightEye, _wayPoints,
                currentPositionIndex, _currentBulletHellStats));
            
        }

        private void StartLaserAttack()
        {
            _leftEye.StopFiring();
            _rightEye.StopFiring();

            if (currentPositionIndex == 1)
            {
                if (UnityEngine.Random.value < 0.5)
                {
                    currentPositionIndex = 0;
                } else
                {
                    currentPositionIndex = 2;
                }

            } else if (currentPositionIndex == 0)
            {
                currentPositionIndex = 2;
            } else
            {
                currentPositionIndex = 0;
            }

            ControlledActor.GoToState(new WallBossMove(ControlledActor, _wayPoints.WayPointList[currentPositionIndex].position,
                _QuickMove));

            BackFromMoveState += FirstLaserAttack;
            return;
        }

        private void FirstLaserAttack()
        {
            BackFromMoveState -= FirstLaserAttack;

            ControlledActor.GoToState(new WallBossLaserAttack(ControlledActor, _leftEye, _rightEye, _wayPoints,
                 currentPositionIndex, _currentLaserAttack));
        }

        private void LaserAttack()
        {

            if (currentPositionIndex == 0)
            {
                currentPositionIndex = 2;
            }
            else
            {
                currentPositionIndex = 0;
            }

            ControlledActor.GoToState(new WallBossMove(ControlledActor, _wayPoints.WayPointList[currentPositionIndex].position,
               _QuickMove));
        }

        private void StartMove()
        {
            int positionIndex = UnityEngine.Random.Range((int)0, (int)3);

            while (positionIndex == currentPositionIndex)
            {
                positionIndex = UnityEngine.Random.Range((int)0, (int)3);
            }

            currentPositionIndex = positionIndex;
            ControlledActor.GoToState(new WallBossMove(ControlledActor, _wayPoints.WayPointList[currentPositionIndex].position,
               _SlowMove));

            var playerTransform = ServiceLocator.Instance.GetNearestPlayer(ControlledActor.transform.position);
        }

        public override InputStates StateId
        {
            get
            {
                return InputStates.WallBoss;
            }
        }
    }
}
