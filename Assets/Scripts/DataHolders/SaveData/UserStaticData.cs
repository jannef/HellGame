using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace fi.tamk.hellgame.dataholders
{
    [Serializable]
    public class RoomSaveDataList : List<RoomSaveData> { }

    [Serializable]
    public static class UserStaticData
    {
        private const string SaveFile = "save.game";
        private static string SavePath { get { return string.Format("{0}/{1}", Application.persistentDataPath, SaveFile); } }

//        [SerializeField] public static RoomSaveDataList RoomData = null;
        [SerializeField] public static RoomSaveDataList RoomData = new RoomSaveDataList();

        public static RoomSaveData GetRoomData(int roomIndex)
        {
            // Uncomment to enable auto load. Also, remove = new RoomSaveDataList() from RoomData declaration
//            if (RoomData == null)
//            {
//                LoadData();
//            }
            return RoomData.Count < 1 ? null : RoomData.DefaultIfEmpty(null).FirstOrDefault(x => x.RoomIndex == roomIndex);
        }

        public static void RoomClearedSave(RoomSaveData data)
        {
            if (RoomData.Count(x => x.RoomIndex == data.RoomIndex) > 0)
            {
                RoomData.First(x => x.RoomIndex == data.RoomIndex).RecordTime = data.RecordTime;
            }
            else
            {
                RoomData.Add(data);
            }

            // Just uncommet to enable autosaving each time data is updated...
//            SaveData();
        }

        public static void SaveData()
        {
            try
            {
                var json = JsonUtility.ToJson(RoomData, true);
                File.WriteAllText(SaveFile, json, Encoding.UTF8);
            }
            catch (Exception e)
            {
                Debug.LogWarning(e);
            }
        }

        public static void LoadData()
        {
            if (File.Exists(SavePath))
            {
                try
                {
                    var json = File.ReadAllText(SavePath, Encoding.UTF8);
                    RoomData = JsonUtility.FromJson<RoomSaveDataList>(json);
                }
                catch (Exception e)
                {
                    Debug.LogWarning(string.Format("Exception raised in saving: {0} -- replacing with empty save.", e));
                    RoomData = new RoomSaveDataList();
                }
            }
            else
            {
                RoomData = new RoomSaveDataList();
            }
        }


    }
}
