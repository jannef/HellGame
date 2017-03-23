//#define DEBUG_TEST
using System.Collections;
using System.Collections.Generic;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.input;
using UnityEngine;
using UnityEngine.SceneManagement;
using fi.tamk.hellgame.dataholders;
using System;

namespace fi.tamk.hellgame.world
{
    /// <summary>
    /// Make sure scene numbers match with those in build!
    /// </summary>
    public enum LegalScenes : int
    {
        ErrorOrNone         = -1,
        GameStaging         = 0,
        SlimeBoss           = 1,
        MobRoom0            = 2,
        MobRoom1            = 3,
        MobRoom4            = 4,
        MainMenu            = 5,
        LevelSelectHub      = 6,
        MobRoom3            = 7,
        WallBoss            = 8
    }

    public static class SceneLoadLock
    {
        public static bool SceneChangeInProgress;
    }

    public sealed class RoomManager : MonoBehaviour
    {
        public static PlayerSaveableData PlayerPersistentData = null;
        public static bool DebugMode;
        public ButtonMap[] Inputs;
        private int currentInputModeIndex = 0;

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
                LoadRoom(LegalScenes.MainMenu);
            }

#if DEBUG_TEST
            StartCoroutine(LoadTest());
#endif
        }

        public static void LoadRoom(LegalScenes whichRoom, bool transitionEffects = true, bool resetPlayerStats = true)
        {
            SceneLoadLock.SceneChangeInProgress = true;

            var player = FindObjectOfType<PlayerLimitBreak>();
            if (player != null)
            {
                PlayerPersistentData = new PlayerSaveableData();
                if (!resetPlayerStats)
                {
                    PlayerPersistentData.Health = player.gameObject.GetComponent<HealthComponent>().Health;
                }
                
                
                PlayerPersistentData.MyConfig = player.gameObject.GetComponent<InputController>().MyConfig;
            }

            var transitionEffect = FindObjectOfType<SceneTransitionEffect>();

            if (transitionEffect != null && transitionEffects)
            {
                transitionEffect.StartSceneTransition((int)whichRoom);
            } else
            {
                SceneManager.LoadScene((int)whichRoom);
            }

            
            SceneManager.sceneLoaded += ReleaseLock;
        }

        private static void ReleaseLock(Scene arg0, LoadSceneMode arg1)
        {
            SceneLoadLock.SceneChangeInProgress = false;
            SceneManager.sceneLoaded -= ReleaseLock;
        }

        private void LateUpdate()
        {
            if (currentInputModeIndex != 1 && Input.GetKeyDown(KeyCode.Joystick1Button0))
            {
                if (Inputs.Length >= 1)
                {
                    SetController(Inputs[1]);
                    currentInputModeIndex = 1;
                }
                else
                    Debug.LogWarning("RoomManager does not have enough buttons schemes set up for debug button scheme change requested (F2)");
            }

            if (currentInputModeIndex != 0 && Input.GetKeyDown(KeyCode.Space))
            {
                if (Inputs.Length >= 1)
                {
                    SetController(Inputs[0]);
                    currentInputModeIndex = 0;
                }
                else
                    Debug.LogWarning("RoomManager does not have enough buttons schemes set up for debug button scheme change requested (F2)");
            }

            if (!DebugMode) return;

            if (Input.GetKeyDown("1"))
            {
                LoadRoom((LegalScenes) 1, false);
            }

            if (Input.GetKeyDown("2"))
            {
                LoadRoom((LegalScenes) 2, false);
            }

            if (Input.GetKeyDown("0"))
            {
                LoadRoom((LegalScenes) 0, false);
            }

            if (Input.GetKeyDown("3"))
            {
                LoadRoom((LegalScenes) 3, false);
            }

            if (Input.GetKeyDown("4"))
            {
                LoadRoom((LegalScenes)4, false);
            }

            if (Input.GetKeyDown("5"))
            {
                LoadRoom((LegalScenes)5, false);
            }

            if (Input.GetKeyDown("6"))
            {
                LoadRoom((LegalScenes)6, false);
            }

            if (Input.GetKeyDown("7"))
            {
                LoadRoom((LegalScenes)7, false);
            }

            if (Input.GetKeyDown("8"))
            {
                LoadRoom((LegalScenes)8, false);
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
#if DEBUG_TEST
        [SerializeField] private int _reloadCount = 0;

        private IEnumerator LoadTest()
        {
            while(true)
            {
                yield return new WaitForSecondsRealtime(0.33f);
                LoadRoom((LegalScenes) Random.Range(1, 3));
                if (Input.GetKey(KeyCode.Delete)) break;
                _reloadCount++;
            }
        }
#endif
    }
}
