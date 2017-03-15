using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.world;
using fi.tamk.hellgame.dataholders;
using UnityEngine;

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

        public WallBossPhaseTransition(ActorComponent controlledHero, WallBossAbstractValues values, float lenght) : base(controlledHero, values)
        {
            var externalObjects = ControlledActor.gameObject.GetComponent<BossExternalObjects>();
            _mobSpawner = externalObjects.ExistingGameObjects[0].GetComponent<AirSpawnerWithSetSpawnPoints>();
            movementStats = UnityEngine.Object.Instantiate(externalObjects.ScriptableObjects[1]) as WallBossMovement;
            BaseValues.phaseNumber++;
            this.lenght = 7f;
        }

        public override void HandleInput(float deltaTime)
        {
            base.HandleInput(deltaTime);

            if (StateTime >= lenght)
            {
                MoveToNextState();
            }
        }

        public override void OnEnterState()
        {
            base.OnEnterState();
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

            /*foreach(SpawnerInstruction ins in _wave.instructions)
            {
                _mobSpawner.Spawn(ins);
            } */

            
        }

        private void MoveToNextState()
        {
            Debug.Log("To new state");
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
