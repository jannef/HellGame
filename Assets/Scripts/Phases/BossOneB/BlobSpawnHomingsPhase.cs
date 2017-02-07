using fi.tamk.hellgame.character;
using fi.tamk.hellgame.phases;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.phases
{
    public class BlobSpawnHomingsPhase : AbstractPhase
    {
        private HealthComponent _myHealth;
        private AirSpawner _mySpawner;
        private float _healthBreakPoint;
        private float _PercentageStep = 15;

        public override void OnBossHealthChange(float healthPercentage, int hitpoints, int maxHp)
        {
            if (hitpoints <= _healthBreakPoint)
            {
                Debug.Log(healthPercentage);
                _healthBreakPoint = hitpoints - (_myHealth.MaxHp / (100 / _PercentageStep));
                _mySpawner.SpawnObjects();
            }
        }

        public BlobSpawnHomingsPhase(BossComponent master) : base(master)
        {
            _myHealth = master.TrackedHealth;
            var go = GameObject.Instantiate(Master.PrefabsUsedByBoss[0]);
            go.transform.position = Master.transform.position;
            go.transform.rotation = Quaternion.identity;
            _mySpawner = go.GetComponent<AirSpawner>();
            _healthBreakPoint = _myHealth.Health - (_myHealth.MaxHp / (100 / _PercentageStep));
        }
    }
}
