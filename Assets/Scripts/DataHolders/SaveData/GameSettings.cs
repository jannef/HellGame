using fi.tamk.hellgame.input;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.dataholders
{
    [Serializable]
    public class GameSettings
    {
        [SerializeField] public ButtonMap GamepadSettings;
        [SerializeField]
        public ButtonMap MouseAndKeyboardSettings;
        [SerializeField]
        public float SFXVolume = .5f;
        [SerializeField]
        public float MusicVolume = .5f;
    }
}
