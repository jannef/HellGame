using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.dataholders
{
    [Serializable]
    public class RoomSaveData
    {
        [SerializeField] public int RoomIndex;
        [SerializeField] public float RecordTime;

        public RoomSaveData(int roomIndex, float recordTime)
        {
            RoomIndex = roomIndex;
            RecordTime = recordTime;
        }
    }
}
