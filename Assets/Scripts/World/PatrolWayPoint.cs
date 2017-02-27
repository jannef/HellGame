using System.Collections;
using System.Collections.Generic;
using fi.tamk.hellgame.utils;
using UnityEngine;

namespace fi.tamk.hellgame.world
{

    public class PatrolWayPoint : MonoBehaviour
    {
        public Transform[] WayPointList { get; private set; }

        public void Awake()
        {
            WayPointList = gameObject.GetAllTranformsFromChildren();
        }
    }
}
