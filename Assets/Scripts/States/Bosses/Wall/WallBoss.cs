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
        private PassiveTurret _leftEye;
        private PassiveTurret _rightEye;
        private AirSpawnerWithSetSpawnPoints _mobSpawner;
        private PatrolWayPoint _wayPoints;
        private int currentPositionIndex = 0;

        private int _phaseNumber = 0;
        private float _movementTimer = 0;
        private float _movementInterval;

        public WallBoss(ActorComponent controlledHero) : base(controlledHero)
        {
            var hc = ControlledActor.GetComponent<HealthComponent>();
            hc.HealthChangeEvent += OnBossHealthChange;
            var externalObjects = ControlledActor.gameObject.GetComponent<BossExternalObjects>();
            _mobSpawner = externalObjects.ExistingGameObjects[0].GetComponent<AirSpawnerWithSetSpawnPoints>();
            _leftEye = externalObjects.ExistingGameObjects[1].GetComponent<PassiveTurret>();
            _rightEye = externalObjects.ExistingGameObjects[2].GetComponent<PassiveTurret>();
            _movementInterval = ControlledActor.ActorNumericData.ActorFloatData[1];
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
                StartMove();
                _movementTimer = 0;
            }
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
                ControlledActor.ActorNumericData.ActorFloatData[0], ControlledActor.ActorNumericData.CurveData[0]));

            var playerTransform = ServiceLocator.Instance.GetNearestPlayer(ControlledActor.transform.position);

            _leftEye.AddGunToFiringList(false, 0);
            _leftEye.AimAtTransform(playerTransform);
            _rightEye.AddGunToFiringList(false, 0);
            _rightEye.AimAtTransform(playerTransform);
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
