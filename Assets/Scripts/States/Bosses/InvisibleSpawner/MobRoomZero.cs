using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.dataholders;
using UnityEngine;

namespace fi.tamk.hellgame.states
{
    class MobRoomZero : StateAbstract
    {
        private SpawnerInstruction _runnerSpawnWave;
        private SpawnerInstruction _turretSpawnWave;
        private AirSpawnerWithSetSpawnPoints _mySpawner;

        private int _activeMinions = 0;
        private int _phase = 0;


        public MobRoomZero(ActorComponent controlledHero) : base(controlledHero)
        {
            var externalObjects = ControlledActor.gameObject.GetComponent<BossExternalObjects>();

            _mySpawner = externalObjects.ExistingGameObjects[0].GetComponent<AirSpawnerWithSetSpawnPoints>();
            _runnerSpawnWave = GameObject.Instantiate(externalObjects.ScriptableObjects[0]) as SpawnerInstruction;
            _turretSpawnWave = GameObject.Instantiate(externalObjects.ScriptableObjects[1]) as SpawnerInstruction;

            SpawnMore();
        }

        private void MinionHasDied()
        {
            _activeMinions--;
            
            if (_activeMinions <= 0)
            {
                Debug.Log("Wave Cleared !");
            }
        }

        private void SpawnMore()
        {
            HealthComponent[] minions;

            minions = _mySpawner.Spawn(_runnerSpawnWave);

            foreach(HealthComponent hc in minions)
            {
                hc.DeathEffect.AddListener(MinionHasDied);
                _activeMinions++;
            }
        }

        protected override void CheckForFalling()
        {
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
