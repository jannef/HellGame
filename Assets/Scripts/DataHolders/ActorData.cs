using UnityEngine;

namespace fi.tamk.hellgame.dataholders
{
    public enum ActorDataMap
    {
        Speed           = 0,
        DashSpeed       = 1,
        DashDuration    = 2,
    }
    public class ActorData : ScriptableObject
    {
        public float[] ActorFloatData;
        public AnimationCurve[] CurveData;
        public GameObject[] GoData;
        public object[] ReferenceCache = new object[4];
    }
}