using fi.tamk.hellgame.dataholders;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.states
{
    public class WallBossLaserAttackStats : ScriptableObject
    {
        public float startDelay;
        public List<WallBossMovement> _movements;
        public float endDelay;
    }
}
