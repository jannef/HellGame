using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.dataholders
{

    public class CameraDollyPoint : MonoBehaviour
    {
        public AnimationCurve MovementToThisCurve;
        public AnimationCurve RotationToThisCurve;
        public float TimeToReachThisPoint = 1f;
    }
}
