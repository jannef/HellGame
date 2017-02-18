using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.phases;
using fi.tamk.hellgame.utils;
using System.Collections;
using System.Collections.Generic;
using fi.tamk.hellgame.dataholders;
using UnityEngine;

namespace fi.tamk.hellgame.phases
{
    public class BlobSpawnTurretsPhase : AbstractPhase
    {
        private ISpawner _mySpawner;
        private SpawnerInstruction _myInstructions;
        private int spawnAmount = 7;
        private float SpawnDelay = 4.4f;
        private float ReductionInSpawndelay = 0.33f;

        private float nextSpawnTime;

        public override void OnUpdate(float deltaTime)
        {
            base.OnUpdate(deltaTime);
            if (PhaseTime < nextSpawnTime) return;

            if (spawnAmount == 0)
            {
                Master.EndAllPhases();
                return;
            }
            else if (spawnAmount == 1)
            {
                SpawnerInstruction instructions = Object.Instantiate(Master.ScriptableObjectsUsedByBoss[0]) as SpawnerInstruction;
                instructions.numberOfSpawns = 12;
                instructions.delayBetweenSpawns = 0.8f;

                var go = Master.ExistingObjectsUsedByBoss[0];
                var spawner = go.GetComponent<AirSpawnerWithSetSpawnPoints>();

                spawner.Spawn(instructions);
                spawnAmount--;
                nextSpawnTime = PhaseTime + SpawnDelay;
                return;
            }

            _mySpawner.Spawn(_myInstructions);
            SpawnDelay -= ReductionInSpawndelay;
            nextSpawnTime = PhaseTime + SpawnDelay;
            spawnAmount--;
        }

        public BlobSpawnTurretsPhase(BossComponent master) : base(master)
        {
            ServiceLocator.Instance.MainCameraScript.RemoveInterest(Master.transform);
            var go = Master.ExistingObjectsUsedByBoss[1];
            _mySpawner = go.GetComponent<AirSpawnerWithSetSpawnPoints>();
            nextSpawnTime = SpawnDelay;
            _myInstructions = Object.Instantiate(Master.ScriptableObjectsUsedByBoss[1]) as SpawnerInstruction;
            Master.BossActor.RequestStateChange(InputStates.BlobResting);
        }
    }
}
