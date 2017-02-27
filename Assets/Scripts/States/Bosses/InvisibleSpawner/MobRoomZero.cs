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
        private SpawnWave[] _spawnWaves;
        private AirSpawnerWithSetSpawnPoints _mySpawner;
        private AirSpawnerWithSetSpawnPoints _myCenterSpawner;

        private int _activeMinions = 0;
        private int _phase = 0;


        public MobRoomZero(ActorComponent controlledHero) : base(controlledHero)
        {
            var externalObjects = ControlledActor.gameObject.GetComponent<BossExternalObjects>();
           
            _mySpawner = externalObjects.ExistingGameObjects[0].GetComponent<AirSpawnerWithSetSpawnPoints>();
            _myCenterSpawner = externalObjects.ExistingGameObjects[1].GetComponent<AirSpawnerWithSetSpawnPoints>();

            var waveTemp = new List<SpawnWave>();

            foreach (ScriptableObject sobj in externalObjects.ScriptableObjects)
            {
                var wave = GameObject.Instantiate(sobj) as SpawnWave;

                waveTemp.Add(wave);
            }

            _spawnWaves = waveTemp.ToArray();

            NextWave();
        }

        private void MinionHasDied()
        {
            _activeMinions--;
            
            if (_activeMinions <= 0)
            {
                NextWave();
            }
        }

        private void NextWave()
        {
            if (_phase >= _spawnWaves.Length)
            {
                Debug.Log("No more waves");
                return;
            }

            foreach (SpawnerInstruction instruction in _spawnWaves[_phase].instructions)
            {
                HealthComponent[] minions;

                if (instruction.preferredSpawner == 0)
                {
                    minions = _mySpawner.Spawn(instruction);
                } else
                {
                    minions = _myCenterSpawner.Spawn(instruction);
                }

                

                foreach (HealthComponent hc in minions)
                {
                    hc.DeathEffect.AddListener(MinionHasDied);
                    _activeMinions++;
                }
            }

            _phase++;
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
