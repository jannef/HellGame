using fi.tamk.hellgame.character;
using fi.tamk.hellgame.input;
using UnityEngine;
using UnityEngine.SceneManagement;
using fi.tamk.hellgame.dataholders;
using fi.tamk.hellgame.ui;

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
        Cellar_1            = 2,
        Cellar_2            = 3,
        Kitchen_1            = 4,
        MainMenu            = 5,
        LevelSelectHub      = 6,
        Kitchen_2            = 7,
        WallBoss            = 8,
        Kitchen_3           = 9,
        Library_1           = 10,
        Library_2           = 11,
        Library_3           = 12,
        Library_Boss        = 13,
        Cellar_3            = 14,
        Chambers_Boss       = 15,
        LevelSelect_Wing_1  = 16,
        Kitchen_LevelSelect = 17,
        Library_LevelSelect = 18,     
    }

    public static class SceneLoadLock
    {
        public static bool SceneChangeInProgress;
    }

    public sealed class RoomManager : MonoBehaviour
    {
        public static PlayerSaveableData PlayerPersistentData = null;
        public static bool DebugMode;
        public ButtonMap[] DefaultInputs;
        private static int currentInputModeIndex = 0;
        public static int LastSceneIndex = 0;
        public static bool RetryFlag = false;

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

            if (UserStaticData.GetGameSettings().MouseAndKeyboardSettings == null)
                UserStaticData.GetGameSettings().MouseAndKeyboardSettings = DefaultInputs[0];
            if (UserStaticData.GetGameSettings().GamepadSettings == null)
                UserStaticData.GetGameSettings().GamepadSettings = DefaultInputs[1];

            if (roomIdentifier.Length == 1)
            {
                DebugMode = true;
                Debug.Log("Debug mode is turned on!");
                MouseTextureToggler.ChangeToGamePlayCursor();
                LoadRoom((LegalScenes) roomIdentifier[0].SceneId);
                Destroy(roomIdentifier[0].gameObject);
            }
            else
            {
                LoadRoom(LegalScenes.MainMenu);
            }
        }

        public static void LoadRoom(LegalScenes whichRoom, bool retryFlag = false, bool transitionEffects = true, bool resetPlayerStats = true)
        {
            SceneLoadLock.SceneChangeInProgress = true;

            LastSceneIndex = SceneManager.GetActiveScene().buildIndex;
            RetryFlag = retryFlag;

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
                if (DefaultInputs.Length >= 1)
                {
                    SetController(UserStaticData.GetGameSettings().GamepadSettings, 1);
                }
                else
                    Debug.LogWarning("RoomManager does not have enough buttons schemes set up for debug button scheme change requested (F2)");
            }

            if (currentInputModeIndex != 0 && Input.GetKeyDown(KeyCode.Space))
            {
                if (DefaultInputs.Length >= 1)
                {
                    SetController(UserStaticData.GetGameSettings().MouseAndKeyboardSettings, 0);
                }
                else
                    Debug.LogWarning("RoomManager does not have enough buttons schemes set up for debug button scheme change requested (F2)");
            }

            //if (!DebugMode) return;

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
                if (DefaultInputs.Length >= 1)
                {
                    SetController(DefaultInputs[0], 0);
                }
                    
                else
                    Debug.LogWarning("RoomManager does not have enough buttons schemes set up for debug button scheme change requested (F1)");
            }

            if (Input.GetKeyDown(KeyCode.F2))
            {
                if (DefaultInputs.Length >= 1)
                {
                    SetController(DefaultInputs[1], 1);
                }
                    
                else
                    Debug.LogWarning("RoomManager does not have enough buttons schemes set up for debug button scheme change requested (F2)");
            }
        }

        public ButtonMap GetControllerBasedOnInputType(Buttons.InputType inputType)
        {
            foreach (ButtonMap map in DefaultInputs)
            {
                if (map.InputType == inputType)
                {
                    return map;
                }
            }

            return null;
        }

        private void SetController(ButtonMap scheme, int index)
        {
            var player = FindObjectOfType<PlayerLimitBreak>();
            if (player != null)
            {
                currentInputModeIndex = 1;
                if (PlayerPersistentData != null) PlayerPersistentData.MyConfig = scheme;
                var input = player.gameObject.GetComponent<InputController>();
                if (input != null) input.MyConfig = scheme;
            }
        }

        public static bool IsQuitting { get; private set; }
        private void OnApplicationQuit()
        {
            IsQuitting = true;
        }
    }
}
