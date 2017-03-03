using System.Collections;
using System.Collections.Generic;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.input;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace fi.tamk.hellgame.world
{
    /// <summary>
    /// Make sure scene numbers match with those in build!
    /// </summary>
    public enum LegalScenes : int
    {
        GameStaging         = 0,
        SlimeBoss           = 1,
        MobRoom0            = 2,
        MobRoom1            = 3
    }

    public static class SceneLoadLock
    {
        public static bool SceneChangeInProgress;
    }

    public sealed class RoomManager : MonoBehaviour
    {
        public bool DebugMode;
        public ButtonMap[] Inputs;

        public LegalScenes CurrentScene { get; private set; }

        private void Awake()
        {
            if (FindObjectsOfType<RoomManager>().Length > 1)
            {
                DestroyImmediate(gameObject);
                return;
            }

            SceneLoadLock.SceneChangeInProgress = false;
            DontDestroyOnLoad(gameObject);
            CurrentScene = LegalScenes.GameStaging;

            var roomIdentifier = FindObjectsOfType<RoomIdentifier>();
            if (SceneManager.GetActiveScene().buildIndex != 0) throw new UnityException("RoomManager instansiated outside GameStaging!");
            if (roomIdentifier.Length > 1) throw new UnityException("Too many RoomIdentifier objects!");

            if (roomIdentifier.Length == 1)
            {
                DebugMode = true;
                Debug.Log("Debug mode is turned on!");
                LoadRoom((LegalScenes) roomIdentifier[0].SceneId);
            }
            else
            {
                LoadRoom(LegalScenes.SlimeBoss);
            }
        }

        public void LoadRoom(LegalScenes whichRoom)
        {
            SceneLoadLock.SceneChangeInProgress = true;
            SceneManager.LoadScene((int) whichRoom);
            SceneManager.sceneLoaded += ReleaseLock;
        }

        private static void ReleaseLock(Scene arg0, LoadSceneMode arg1)
        {
            SceneLoadLock.SceneChangeInProgress = false;
            SceneManager.sceneLoaded -= ReleaseLock;
        }

        private void LateUpdate()
        {
            if (!DebugMode) return;

            if (Input.GetKeyDown("1"))
            {
                LoadRoom((LegalScenes) 1);
            }

            if (Input.GetKeyDown("2"))
            {
                LoadRoom((LegalScenes) 2);
            }

            if (Input.GetKeyDown("0"))
            {
                LoadRoom((LegalScenes) 0);
            }

            if (Input.GetKeyDown("3"))
            {
                LoadRoom((LegalScenes) 3);
            }

            if (Input.GetKeyDown(KeyCode.F1))
            {
                if (Inputs.Length >= 1)
                    SetController(Inputs[0]);
                else
                    Debug.LogWarning("RoomManager does not have enough buttons schemes set up for debug button scheme change requested (F1)");
            }

            if (Input.GetKeyDown(KeyCode.F2))
            {
                if (Inputs.Length >= 1)
                    SetController(Inputs[1]);
                else
                    Debug.LogWarning("RoomManager does not have enough buttons schemes set up for debug button scheme change requested (F2)");
            }
        }

        private void SetController(ButtonMap scheme)
        {
            var player = FindObjectOfType<PlayerLimitBreak>();
            if (player != null)
            {
                var input = player.gameObject.GetComponent<InputController>();
                if (input != null) input.MyConfig = scheme;
            }
        }
    }
}
