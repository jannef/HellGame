﻿using fi.tamk.hellgame.dataholders;
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
        public UnityEvent RoomFirstTimeOpen;
        public UnityEvent RoomNotEnteredEvent;
        [SerializeField] private LegalScenes targetRoom;
        [SerializeField] private LegalScenes theRoomBeforeInProgression;
        [SerializeField] private bool OpenEvenIfRoomNotFound = false;
        [SerializeField] private bool ShowHaloIfOpenedAutomatically = false;

        // Use this for initialization
        void Start()
        {

            var roomData = UserStaticData.GetRoomData((int)targetRoom);

            if (OpenEvenIfRoomNotFound)
            {
                if (RoomFoundInSaveData != null) RoomFoundInSaveData.Invoke();
                if (ShowHaloIfOpenedAutomatically && roomData == null)
                {
                    if (RoomNotEnteredEvent != null) RoomNotEnteredEvent.Invoke();
                }
                return;
            }

            if (roomData == null)
            {
                roomData = UserStaticData.GetRoomData((int)theRoomBeforeInProgression);
                if (roomData != null)
                {
                    if (!UserStaticData.GetIfRoomAlreadyOpenedOnce((int) targetRoom))
                    {
                        if (RoomFirstTimeOpen != null) RoomFirstTimeOpen.Invoke();
                        if (RoomNotEnteredEvent != null) RoomNotEnteredEvent.Invoke();
                        return;
                    } else
                    {
                        if (RoomNotEnteredEvent != null) RoomNotEnteredEvent.Invoke();
                        if (RoomFoundInSaveData != null) RoomFoundInSaveData.Invoke();
                    }
                }
            } else
            {
                if (RoomFoundInSaveData != null) RoomFoundInSaveData.Invoke();
            }
        }
    }
}
