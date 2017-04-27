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
        [SerializeField] private LegalScenes theRoomBeforeInProgression;
        [SerializeField] private bool OpenEvenIfRoomNotFound = false;

        // Use this for initialization
        void Start()
        {
            if (OpenEvenIfRoomNotFound)
            {
                if (RoomFoundInSaveData != null) RoomFoundInSaveData.Invoke();
                return;
            }

            var roomData = UserStaticData.GetRoomData((int)targetRoom);

            if (roomData == null)
            {
                roomData = UserStaticData.GetRoomData((int)theRoomBeforeInProgression);
            }

            if (roomData != null)
            {
                if (RoomFoundInSaveData != null) RoomFoundInSaveData.Invoke();
            }
        }
    }
}
