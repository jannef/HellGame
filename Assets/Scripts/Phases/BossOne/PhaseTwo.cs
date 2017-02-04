using System;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.utils.Stairs.Utils;
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
                Debug.Log("Boss is about to die! Survive it's desperate final attack in phase 3!");
                Master.EnterPhase(new PhaseThree(Master));
            }
        }

        public override void OnUpdate(float deltaTime)
        {
            base.OnUpdate(deltaTime);

            if (PhaseTime > 32f)
            {
                Debug.Log("Boss summons reinforcements!");
                _mySpawner.SpawnObjects();
                PhaseTime = 0f;
            }
        }

        public override void OnExitPhase()
        {
            Pool.DelayedDestroyGo(_mySpawner.gameObject);
        }

        public PhaseTwo(BossComponent master) : base(master)
        {
            Master.TrackedHealth.AllowDeath = false;
            var go = GameObject.Instantiate(Master.PrefabsUsedByBoss[0]);
            go.transform.position = Master.transform.position;
            go.transform.rotation = Quaternion.identity;
            _mySpawner = go.GetComponent<AirSpawner>();

            PhaseTime = 30f;
        }
    }
}