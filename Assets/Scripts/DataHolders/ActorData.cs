using UnityEngine;

namespace fi.tamk.hellgame.dataholders
{
    public enum ActorDataMap
    {
        Speed           = 0,
        DashSpeed       = 1,
        DashDuration    = 2
    }
    public class ActorData : ScriptableObject
    {
        public float[] ActorFloatData;
        public AnimationCurve[] CurveData;
    }
}