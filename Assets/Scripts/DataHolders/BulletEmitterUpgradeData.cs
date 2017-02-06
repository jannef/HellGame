using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.dataholders
{
    [System.Serializable]
    public class BulletEmitterUpgradeData
    {
        public int UpgradePointLimitIncrease = 10;
        public float AddedEmitterCooldown = 0f;
        public int AddedBulletNumber;
        public float AddedSpread = 0f;
        public float AddedDamage;
        public float AddedDispersion = 0f;
        public float AddedSpeed = 0f;
    }
}
