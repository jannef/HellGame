﻿using System.Collections.Generic;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.dataholders;
using fi.tamk.hellgame.world;
using UnityEngine;
using fi.tamk.hellgame.utils;

namespace fi.tamk.hellgame.states
{
    class MobRoomZero : StateAbstract
    {
        private SpawnWave[] _spawnWaves;
        private AirSpawnerWithSetSpawnPoints _mySpawner;
        private AirSpawnerWithSetSpawnPoints _myCenterSpawner;
        private PatrolWayPoint[] _patrolWayPoint;
        private TransitionTrigger _transitionTrigger;

        private int _activeMinions = 0;
        private int _phase = 0;
        private float StartDelay = 0f;

        public MobRoomZero(ActorComponent controlledHero) : base(controlledHero)
        {
            var externalObjects = ControlledActor.gameObject.GetComponent<BossExternalObjects>();

            _patrolWayPoint = externalObjects.ExistingGameObjects[2].GetComponentsInChildren<PatrolWayPoint>();
           
            _mySpawner = externalObjects.ExistingGameObjects[0].GetComponent<AirSpawnerWithSetSpawnPoints>();
            _myCenterSpawner = externalObjects.ExistingGameObjects[1].GetComponent<AirSpawnerWithSetSpawnPoints>();

            var waveTemp = new List<SpawnWave>();

            foreach (ScriptableObject sobj in externalObjects.ScriptableObjects)
            {
                var wave = GameObject.Instantiate(sobj) as SpawnWave;

                waveTemp.Add(wave);
            }

            if (ControlledActor.ActorNumericData != null)
            {
                if (ControlledActor.ActorNumericData != null && ControlledActor.ActorNumericData.ActorFloatData.Length > 0)
                {
                    StartDelay = ControlledActor.ActorNumericData.ActorFloatData[0];
                }
            }

            _spawnWaves = waveTemp.ToArray();
            ControlledActor.StartCoroutine(StaticCoroutines.DoAfterDelay(0.2f, AddToEncounterEvent));
        }

        private void AddToEncounterEvent()
        {
            RoomIdentifier.EncounterBegin += NextWave;
        }

        private void MinionHasDied()
        {
            _activeMinions--;
            
            if (_activeMinions <= 0)
            {
                NextWave();
            }
        }

        private void EndEncounter()
        {
            RoomIdentifier.OnRoomCompleted();
        }

        private void NextWave()
        {
            if (_phase >= _spawnWaves.Length)
            {
                EndEncounter();
                return;
            }

            foreach (SpawnerInstruction instruction in _spawnWaves[_phase].instructions)
            {
                HealthComponent[] minions;

                if (instruction.preferredSpawner == 0)
                {
                    minions = _mySpawner.Spawn(instruction);
                    ;
                } else
                {
                    minions = _myCenterSpawner.Spawn(instruction);
                }

                foreach (var hc in minions)
                {
                    hc.DeathEffect.AddListener(MinionHasDied);
                    _activeMinions++;


                    // This little rig is responsible for setting waypoints for patrolling enemies. See PatrolWayPoints for 
                    var ac = hc.gameObject.GetComponent<ActorComponent>();
                    if (ac != null && ac.ActorNumericData != null && ac.ActorNumericData.GoData.Length > 0)
                    {
                        
                        ac.ActorNumericData.GoData[0] = _patrolWayPoint[0].gameObject;
                    }
                }
            }

            _phase++;
        }

        public override InputStates StateId
        {
            get
            {
                return InputStates.MobRoomZero;
            }
        }
    }
}
