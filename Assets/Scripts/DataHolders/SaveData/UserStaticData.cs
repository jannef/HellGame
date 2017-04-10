using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

namespace fi.tamk.hellgame.dataholders
{
    [Serializable]
    public static class UserStaticData
    {
        private const string SaveFile = "save.game";
        private static string SavePath { get { return string.Format("{0}/{1}", Application.persistentDataPath, SaveFile); } }

        public static List<RoomSaveData> RoomData = null;

        static UserStaticData()
        {
            LoadData();
        }

        public static RoomSaveData GetRoomData(int roomIndex)
        {
            if (RoomData == null)
            {
                LoadData();
            }

            return RoomData.Count < 1 ? null : RoomData.DefaultIfEmpty(null).OrderBy(x => x.RecordTime).FirstOrDefault(x => x.RoomIndex == roomIndex);
        }

        public static void RoomClearedSave(RoomSaveData data)
        {
            RoomData.Add(data);
            SaveData();
        }

        public static void SaveData()
        {
            var fs = new MemoryStream();
            try
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(fs, RoomData);
                File.WriteAllBytes(SavePath, fs.GetBuffer());
            }
            catch (Exception e)
            {
                Debug.LogWarning(e);
            }
            finally
            {
                fs.Close();
            }
        }

        public static void LoadData()
        {
            if (!File.Exists(SavePath)) RoomData = new List<RoomSaveData>();

            try
            {
                byte[] bytes = File.ReadAllBytes(SavePath);
                using (var fs = new MemoryStream(bytes))
                {
                    var formatter = new BinaryFormatter();
                    RoomData = formatter.Deserialize(fs) as List<RoomSaveData>;
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning(e);
                RoomData = new List<RoomSaveData>();
            }
        }
    }
}
