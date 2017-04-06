using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.dataholders
{

    public class SlimeJumpData : ScriptableObject
    {

        public float JumpDelay = 2f;
        public float JumpSpeed = 33f;
        public float JumpHeight = 10f;
        public float TargetJumpLenght = 33f;
        public float TargetjumpLenghtStrenght = 0.66f;
        public AnimationCurve SquishCurve;
    }
}
