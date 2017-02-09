using fi.tamk.hellgame.character;
using fi.tamk.hellgame.utils;
using UnityEngine;

namespace fi.tamk.hellgame.phases.bossone
{
    public class PhaseTwo : AbstractPhase
    {
        private AirSpawner _mySpawner;
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
                _mySpawner.SpawnObjects();
                PhaseTime = 0f;
            }

            _mySpawner.SpawnObjects();
        }

        public override void OnExitPhase()
        {
            var go = _mySpawner.gameObject;
            Pool.Instance.ReturnObject(ref go);
        }

        public PhaseTwo(BossComponent master) : base(master)
        {
            Master.TrackedHealth.AllowDeath = false;
            _mySpawner = InstantiateSpawner(master.PrefabsUsedByBoss[0]);

            PhaseTime = 30f;
        }
    }
}