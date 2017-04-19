﻿using fi.tamk.hellgame.world;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace fi.tamk.hellgame.ui
{

    public class RoomLoadingButton : MonoBehaviour
    {
        [SerializeField] private bool _targetCurrentScene;
        [SerializeField] private LegalScenes targetScene;

        private void Start()
        {
            var button = GetComponent<Button>();
            if (_targetCurrentScene)
            {
                button.onClick.AddListener(TargetCurrentScene);
            } else
            {
                button.onClick.AddListener(Activate);
            }
        }

        private void TargetCurrentScene()
        {
            RoomManager.LoadRoom((LegalScenes) SceneManager.GetActiveScene().buildIndex, true);
        }

        private void Activate()
        {
            if(targetScene != LegalScenes.ErrorOrNone)
                    RoomManager.LoadRoom(targetScene);
                else
                    throw new UnityException("TransitionTrigger with incorrectly set target was activated!");
        }
    }
}
