using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.dataholders
{

    public class RoomSaveData
    {

        public int roomIndex;
        public float recordTime;

        public RoomSaveData(int roomIndex, float recordTime)
        {
            this.roomIndex = roomIndex;
            this.recordTime = recordTime;
        }
    }
}
