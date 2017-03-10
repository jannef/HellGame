using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using UnityEngine;
using fi.tamk.hellgame.world;
using fi.tamk.hellgame.utils;

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
        private AttackState _currentAttackState = AttackState.BulletHell;

        private int _phaseNumber = 0;
        private float _movementTimer = 0;
        private float _movementInterval;

        private int _wallRunAmount = 0;

        public WallBoss(ActorComponent controlledHero) : base(controlledHero)
        {
            var hc = ControlledActor.GetComponent<HealthComponent>();
            hc.HealthChangeEvent += OnBossHealthChange;
            var externalObjects = ControlledActor.gameObject.GetComponent<BossExternalObjects>();
            _mobSpawner = externalObjects.ExistingGameObjects[0].GetComponent<AirSpawnerWithSetSpawnPoints>();
            _leftEye = externalObjects.ExistingGameObjects[1].GetComponent<PassiveTurret>();
            _rightEye = externalObjects.ExistingGameObjects[2].GetComponent<PassiveTurret>();
            _movementInterval = 0.22f;
            _wayPoints = externalObjects.ExistingGameObjects[3].GetComponent<PatrolWayPoint>();

        }

        private void OnBossHealthChange(float percentage, int health, int maxHealth)
        {
                    
        }

        public override void HandleInput(float deltaTime)
        {
            base.HandleInput(deltaTime);
            _movementTimer += deltaTime;

            if (_movementTimer >= _movementInterval)
            {
                Reason();
                _movementTimer = 0;
            }
        }

        private void Reason()
        {
            switch (_currentAttackState)
            {
                case AttackState.BulletHell:
                    StartLaserAttack();
                    _movementInterval = .1f;
                    _currentAttackState = AttackState.LaserHop;
                    break;
                case AttackState.LaserHop:
                    _movementInterval = 10f;
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
            StartMove();
            
            var playerTransform = ServiceLocator.Instance.GetNearestPlayer(ControlledActor.transform.position);
            _leftEye.AddGunToFiringList(true, 0);
            _leftEye.AimAtTransform(playerTransform);
            _rightEye.AddGunToFiringList(true, 0);
            _rightEye.AimAtTransform(playerTransform);
        }

        private void StartLaserAttack()
        {
            _wallRunAmount = 4;
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

                ControlledActor.GoToState(new WallBossMove(ControlledActor, _wayPoints.WayPointList[currentPositionIndex].position,
                ControlledActor.ActorNumericData.ActorFloatData[0], ControlledActor.ActorNumericData.CurveData[0], 0f));

                BackFromMoveState += FirstLaserAttack;
                return;
            }

            FirstLaserAttack();
            return;
        }

        private void FirstLaserAttack()
        {
            BackFromMoveState -= FirstLaserAttack;

            if (currentPositionIndex == 0)
            {
                currentPositionIndex = 2;
            } else
            {
                currentPositionIndex = 0;
            }

            ControlledActor.GoToState(new WallBossMove(ControlledActor, _wayPoints.WayPointList[currentPositionIndex].position,
               ControlledActor.ActorNumericData.ActorFloatData[0], ControlledActor.ActorNumericData.CurveData[0], .66f));

            StopFiringLasers += _leftEye.StartFiringLaser(1);
            _leftEye.StopAiming();
            StopFiringLasers += _rightEye.StartFiringLaser(1);
            _rightEye.StopAiming();
            BackFromMoveState += LaserAttack;
        }

        private void LaserAttack()
        {
            _wallRunAmount--;
            if (_wallRunAmount <= 0)
            {
                BackFromMoveState -= LaserAttack;
                if (StopFiringLasers != null) StopFiringLasers.Invoke();
                StopFiringLasers = null;
                return;
            }

            if (currentPositionIndex == 0)
            {
                currentPositionIndex = 2;
            }
            else
            {
                currentPositionIndex = 0;
            }

            ControlledActor.GoToState(new WallBossMove(ControlledActor, _wayPoints.WayPointList[currentPositionIndex].position,
               ControlledActor.ActorNumericData.ActorFloatData[0], ControlledActor.ActorNumericData.CurveData[0], 0));
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
                ControlledActor.ActorNumericData.ActorFloatData[0], ControlledActor.ActorNumericData.CurveData[0], 3));

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
