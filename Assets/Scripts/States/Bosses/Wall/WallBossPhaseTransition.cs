using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.world;
using fi.tamk.hellgame.dataholders;
using UnityEngine;
using fi.tamk.hellgame.utils;

namespace fi.tamk.hellgame.states
{
    class WallBossPhaseTransition : WallBossAbstract
    {
        AirSpawnerWithSetSpawnPoints _mobSpawner;
        PatrolWayPoint _wayPointList;
        private float lenght;
        private WallBossMovement movementStats;
        private SpawnWave _wave;
        private int _targetPhase;
        private SpawnWave _spawnWave;
        private PassiveTurret _leftEye;
        private PassiveTurret _rightEye;
        bool hasStoppedTransitionEffects = false;

        public WallBossPhaseTransition(ActorComponent controlledHero, WallBossAbstractValues values, WallBossTransitionPhaseStats stats) : base(controlledHero, values)
        {
            var externalObjects = ControlledActor.gameObject.GetComponent<BossExternalObjects>();
            _mobSpawner = externalObjects.ExistingGameObjects[0].GetComponent<AirSpawnerWithSetSpawnPoints>();
            movementStats = UnityEngine.Object.Instantiate(externalObjects.ScriptableObjects[1]) as WallBossMovement;
            BaseValues.phaseNumber++;
            this.lenght = stats._timeOfState;
            this._spawnWave = stats._spawnWave;
            _leftEye = externalObjects.ExistingGameObjects[1].GetComponent<PassiveTurret>();
            _rightEye = externalObjects.ExistingGameObjects[2].GetComponent<PassiveTurret>();
        }

        public override void HandleInput(float deltaTime)
        {
            base.HandleInput(deltaTime);

            if (StateTime >= lenght)
            {
                MoveToNextState();
            } else if (!hasStoppedTransitionEffects && StateTime >= lenght - 2.0f)
            {
                var PhaseEndedEvent = ControlledActor.GetComponent<BossStateCompletedEvent>();
                if (PhaseEndedEvent != null) PhaseEndedEvent.TransitionPhaseCompleted();
                hasStoppedTransitionEffects = true;
            }
        }

        public override void OnEnterState()
        {
            base.OnEnterState();

            var PhaseEndedEvent = ControlledActor.GetComponent<BossStateCompletedEvent>();
            if (PhaseEndedEvent != null) PhaseEndedEvent.PhaseCompleted();

            var playerTransform = ServiceLocator.Instance.GetNearestPlayer(ControlledActor.transform.position);
            _leftEye.AimAtTransform(playerTransform);
            _rightEye.AimAtTransform(playerTransform);
            _leftEye.StopFiring();
            _rightEye.StopFiring();

            if (BaseValues.currentPositionIndex != 1)
            {
                BaseValues.currentPositionIndex = 1;
                ControlledActor.GoToState(new WallBossMove(ControlledActor, BaseValues, 
                    BaseValues.wayPointList.WayPointList[BaseValues.currentPositionIndex].position,
               movementStats));
            } else
            {
                OnResumeState();
            }           
        }

        public override void OnResumeState()
        {
            base.OnResumeState();

            foreach(SpawnerInstruction ins in _spawnWave.instructions)
            {
                _mobSpawner.Spawn(ins);
            }

            
        }

        private void MoveToNextState()
        {
            ControlledActor.GoToState(new WallBoss(ControlledActor, BaseValues));
        }

        public override InputStates StateId
        {
            get
            {
                return InputStates.WallBossTransition;
            }
        }
    }
}
