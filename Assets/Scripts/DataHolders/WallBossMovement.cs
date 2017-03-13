using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.dataholders
{

    public class WallBossMovement : ScriptableObject
    {
        public float MovementSpeed;
        public AnimationCurve MovementCurve;
        public float MovementDelay;
    }
}
