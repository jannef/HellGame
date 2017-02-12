using fi.tamk.hellgame.character;
using fi.tamk.hellgame.phases;
using fi.tamk.hellgame.utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.phases
{
    public class BlowFirstSpanwPhase : AbstractPhase
    {
        private HealthComponent _myHealth;
        private AirSpawnerWithSetSpawnPoints _mySpawner;
        private SpawnerInstruction _instructions;

        private float currentInterval = 9.2f;
        private int wavesSpawned = 0;
        private int waveNumber = 5;
        private float timer = 5;
        
        

        public override void OnBossHealthChange(float healthPercentage, int hitpoints, int maxHp)
        {
        }

        public override void OnUpdate(float deltaTime)
        {
            base.OnUpdate(deltaTime);
            timer += Time.deltaTime;

            if (timer >= currentInterval)
            {
                _instructions.numberOfSpawns++;
                _mySpawner.Spawn(_instructions);
                timer = 0;
                wavesSpawned++;

                if (wavesSpawned == waveNumber)
                {
                    Master.EndAllPhases();
                    Master.EnterPhase(new BlobSecondFirePhase(Master));
                }
            } 
        }

        public override void OnMinionDeath(MinionComponent whichMinion)
        {
            base.OnMinionDeath(whichMinion);
        }

        public BlowFirstSpanwPhase(BossComponent master) : base(master)
        {
            ServiceLocator.Instance.MainCameraScript.RemoveInterest(Master.transform);
            _mySpawner = Master.ExistingObjectsUsedByBoss[0].GetComponent<AirSpawnerWithSetSpawnPoints>();
            _instructions = Object.Instantiate(Master.ScriptableObjectsUsedByBoss[0]) as SpawnerInstruction;
        }
    }
}
