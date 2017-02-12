using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.phases;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.phases
{
    public class BlobSpawnTurretsPhase : AbstractPhase
    {
        private HealthComponent _myHealth;
        private ISpawner _mySpawner;
        private SpawnerInstruction _myInstructions;
        private int spawnAmount = 8;
        private float SpawnDelay = 2.2f;
        private float ReductionInSpawndelay = 0.2f;

        private float nextSpawnTime;

        public override void OnUpdate(float deltaTime)
        {
            base.OnUpdate(deltaTime);
            if (PhaseTime >= nextSpawnTime)
            {
                _mySpawner.Spawn(_myInstructions);
                SpawnDelay -= ReductionInSpawndelay;
                nextSpawnTime = PhaseTime + SpawnDelay;
                spawnAmount--;

                if (spawnAmount == 0)
                {
                    Master.EndAllPhases();
                }
            }

        }

        public BlobSpawnTurretsPhase(BossComponent master) : base(master)
        {
            _myHealth = master.TrackedHealth;
            var go = Master.ExistingObjectsUsedByBoss[1];
            _mySpawner = go.GetComponent<AirSpawnerWithSetSpawnPoints>();
            nextSpawnTime = SpawnDelay;
            _myInstructions = Object.Instantiate(Master.ScriptableObjectsUsedByBoss[1]) as SpawnerInstruction;
        }
    }
}
