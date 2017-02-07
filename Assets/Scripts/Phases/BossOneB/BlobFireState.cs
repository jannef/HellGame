using fi.tamk.hellgame.character;
using fi.tamk.hellgame.phases;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.phases
{
    public class BlobFireState : AbstractPhase
    {
        private HealthComponent _myHealth;
        private static bool _hasActivatedSecondBlobPhase = false;

        public override void OnBossHealthChange(float healthPercentage, int hitpoints, int maxHp)
        {
            if (!_hasActivatedSecondBlobPhase && hitpoints <= maxHp * .8)
            {
                _hasActivatedSecondBlobPhase = true;
                Master.EnterPhase(new BlobSpawnTurretsPhase(Master));
            }
        }

        public BlobFireState(BossComponent master) : base(master)
        {
            _myHealth = master.TrackedHealth;
            if (_myHealth.Health <= _myHealth.MaxHp / 2)
            {
                Master.BossActor.RequestStateChange(interfaces.InputStates.BlobSecond);
            }

            Master.EnterPhase(new BlobSpawnHomingsPhase(master));
        }
    }
}
