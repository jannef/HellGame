using fi.tamk.hellgame.character;
using fi.tamk.hellgame.utils;
using UnityEngine;

namespace fi.tamk.hellgame.phases.bossone
{
    public class PhaseTwo : AbstractPhase
    {
        private AirSpawnerWithSetSpawnPoints _mySpawner;
        private SpawnerInstruction _mySpawnData;

        public override void OnBossHealthChange(float healthPercentage, int hitpoints, int maxHp)
        {
            if (healthPercentage <= 0)
            {
                Master.EnterPhase(new PhaseThree(Master));
                Master.RemovePhase(this);
            }
        }

        public override void OnUpdate(float deltaTime)
        {
            base.OnUpdate(deltaTime);

            if (PhaseTime > 32f)
            {
                _mySpawner.Spawn(_mySpawnData);
                PhaseTime = 0f;
            }
        }

        public override void OnExitPhase()
        {
            var go = _mySpawner.gameObject;
            Pool.Instance.ReturnObject(ref go);
        }

        public PhaseTwo(BossComponent master) : base(master)
        {
            Master.TrackedHealth.AllowDeath = false;
            _mySpawner = Master.ExistingObjectsUsedByBoss[0].GetComponent<AirSpawnerWithSetSpawnPoints>();
            _mySpawnData = Master.ScriptableObjectsUsedByBoss[0] as SpawnerInstruction;

            PhaseTime = 30f;
        }
    }
}