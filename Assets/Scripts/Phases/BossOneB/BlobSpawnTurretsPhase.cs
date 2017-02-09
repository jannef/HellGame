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
        private float _healthBreakPoint;
        private float _PercentageStep = 33;

        public override void OnBossHealthChange(float healthPercentage, int hitpoints, int maxHp)
        {
            if (hitpoints <= _healthBreakPoint)
            {
                Debug.Log(healthPercentage);
                _healthBreakPoint = hitpoints - (_myHealth.MaxHp / (100 / _PercentageStep));
                //_mySpawner.Spawn();
            }
        }

        public BlobSpawnTurretsPhase(BossComponent master) : base(master)
        {
            _myHealth = master.TrackedHealth;
            var go = Master.ExistingObjectsUsedByBoss[0];
            _mySpawner = go.GetComponent<AirSpawnerWithSetSpawnPoints>();
            _healthBreakPoint = _myHealth.Health - (_myHealth.MaxHp / (100 / _PercentageStep));
        }
    }
}
