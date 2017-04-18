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
        public ButtonMap GamepadSettings;
        public ButtonMap MouseAndKeyboardSettings;
        public float SFXVolume = .5f;
        public float MusicVolume = .5f;
    }
}
