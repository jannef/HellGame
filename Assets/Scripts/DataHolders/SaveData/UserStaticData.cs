using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.dataholders
{

    public static class UserStaticData
    {
        public static List<RoomSaveData> RoomData = new List<RoomSaveData>();

        public static RoomSaveData GetRoomData(int roomIndex)
        {
            return RoomData.Count < 1 ? null : RoomData.DefaultIfEmpty(null).First(x => x.roomIndex == roomIndex);
        }

        public static void RoomClearedSave(RoomSaveData data)
        {
            var existingData = RoomData.Count < 1 ? null : RoomData.DefaultIfEmpty(null).First(x => x.roomIndex == data.roomIndex);

            if (existingData == null)
            {
                RoomData.Add(data);
            } else
            {
                if (existingData.recordTime < data.recordTime)
                {
                    RoomData.Add(data);
                }
            }
        }
    }
}
