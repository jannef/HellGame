using fi.tamk.hellgame.character;
using fi.tamk.hellgame.utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.phases
{
    public class BlobSecondFirePhase : AbstractPhase
    {
        private static bool _hasActivatedSecondBlobPhase = false;

        public override void OnBossHealthChange(float healthPercentage, int hitpoints, int maxHp)
        {
            if (hitpoints <= maxHp * 0.5f && !_hasActivatedSecondBlobPhase)
            {
                _hasActivatedSecondBlobPhase = true;
                Master.EnterPhase(new BlobSpawnTurretsPhase(Master));
                Master.RemovePhase(this);
            }
        }

        public BlobSecondFirePhase(BossComponent master) : base(master)
        {
            ServiceLocator.Instance.MainCameraScript.AddInterest(new CameraInterest(Master.transform, 0.5f));
            Master.BossActor.RequestStateChange(interfaces.InputStates.BlobThird);
        }
    }
}
