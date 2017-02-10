using fi.tamk.hellgame.character;
using fi.tamk.hellgame.phases;
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

        private float beginnigInterval = 12f;
        private float finalInterval = 6.66f;
        private float currentInterval = 12f;
        private float rampUpTime = 12f;
        
        

        public override void OnBossHealthChange(float healthPercentage, int hitpoints, int maxHp)
        {
            _mySpawner.Spawn(_instructions);
        }

        public override void OnUpdate(float deltaTime)
        {
            base.OnUpdate(deltaTime);
        }

        public override void OnMinionDeath(MinionComponent whichMinion)
        {
            base.OnMinionDeath(whichMinion);
        }

        public BlowFirstSpanwPhase(BossComponent master) : base(master)
        {
            _mySpawner = Master.ExistingObjectsUsedByBoss[0].GetComponent<AirSpawnerWithSetSpawnPoints>();
            _instructions = Object.Instantiate(Master.ScriptableObjectsUsedByBoss[0]) as SpawnerInstruction;
        }
    }
}
