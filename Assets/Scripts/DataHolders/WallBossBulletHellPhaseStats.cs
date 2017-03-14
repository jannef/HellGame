using fi.tamk.hellgame.dataholders;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.dataholders
{

    public class WallBossBulletHellPhaseStats : ScriptableObject
    {
        public int[] LeftEyeTurrets;
        public int[] RightEyeTurret;
        public float MovementTime;
        public WallBossMovement MovementStats;
        public float PhaseLenght;
    }
}
