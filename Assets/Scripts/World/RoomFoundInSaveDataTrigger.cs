using fi.tamk.hellgame.dataholders;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace fi.tamk.hellgame.world
{

    public class RoomFoundInSaveDataTrigger : MonoBehaviour
    {
        public UnityEvent RoomFoundInSaveData;
        [SerializeField] private LegalScenes targetRoom;


        // Use this for initialization
        void Awake()
        {
            SceneManager.sceneLoaded += Initialize;
        }

        private void Initialize(Scene scene, LoadSceneMode mode)
        {
            SceneManager.sceneLoaded -= Initialize;
            var roomData = UserStaticData.GetRoomData((int)targetRoom);

            if (roomData != null)
            {
                if (RoomFoundInSaveData != null) RoomFoundInSaveData.Invoke();
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
