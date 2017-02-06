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
        }

        public override void OnExitPhase()
        {
            var go = _mySpawner.gameObject;
            Pool.Instance.ReturnObject(ref go);
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