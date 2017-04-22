using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using FMODUnity;
using FMOD;

namespace fi.tamk.hellgame.dataholders
{
    [Serializable]
    public static class UserStaticData
    {
        private const string SaveFile = "save.game";
        private const string GameSettingsFile = "settings.game";
        private static string SavePath { get { return string.Format("{0}/{1}", Application.persistentDataPath, SaveFile); } }
        private static string SettingsSavePath { get { return string.Format("{0}/{1}", Application.persistentDataPath, GameSettingsFile); } }

        public static List<RoomSaveData> RoomData = null;
        public static GameSettings Settings;

        static UserStaticData()
        {
            LoadData();
            if (Settings == null) return;
        }

        public static RoomSaveData GetRoomData(int roomIndex)
        {
            if (RoomData == null)
            {
                LoadData();
            }

            return RoomData.Count < 1 ? null : RoomData.DefaultIfEmpty(null).OrderBy(x => x.RecordTime).FirstOrDefault(x => x.RoomIndex == roomIndex);
        }

        public static GameSettings GetGameSettings()
        {
            if (Settings == null)
            {
                UnityEngine.Debug.Log("Settings is null");
                LoadData();
            }

            return Settings;
        }

        public static void SaveGameSettings()
        {

            try
            {
                Settings.BeforeSerialization();
                var blob = JsonUtility.ToJson(Settings, true);
                File.WriteAllText(SettingsSavePath, blob);
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogWarning(e);
            }
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
                UnityEngine.Debug.LogWarning(e);
            }
            finally
            {
                fs.Close();
            }
        }

        public static void SetMusicMixerVolume(float value)
        {
            Settings.MusicVolume = value;

            RuntimeManager.GetVCA("vca:/music").setVolume(value);
        }

        public static void SetSFXMixerVolume(float value)
        {
            Settings.SFXVolume = value;

            RuntimeManager.GetVCA("vca:/sfx").setVolume(value);
        }

        public static void LoadData()
        {
            if (!File.Exists(SavePath)) RoomData = new List<RoomSaveData>();
            if (!File.Exists(SettingsSavePath)) Settings = new GameSettings();

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
                UnityEngine.Debug.LogWarning(e);
                RoomData = new List<RoomSaveData>();
            }

            try
            {
                string bytes = File.ReadAllText(SettingsSavePath);
                Settings =  JsonUtility.FromJson<GameSettings>(bytes);
                Settings.AfterDeSerialization();
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogWarning(e);
                Settings = new GameSettings();
            }
        }
    }
}
