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
            if (hitpoints <= maxHp * 0.9f && !_hasActivatedSecondBlobPhase)
            {
                _hasActivatedSecondBlobPhase = true;
                Master.EnterPhase(new BlowFirstSpanwPhase(Master));
                Master.BossActor.RequestStateChange(interfaces.InputStates.Obstacle);
                Master.RemovePhase(this);
            }
        }

        public BlobFireState(BossComponent master) : base(master)
        {
            _myHealth = master.TrackedHealth;
        }
    }
}
