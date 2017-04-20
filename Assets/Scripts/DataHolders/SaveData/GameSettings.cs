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
        public ButtonMap GamepadSettings
        {
            get
            {
                return _gb ?? (_gb = ButtonMap.FromSerializedForm(_gamepadSettings));
            }

            set
            {
                _gb = value;
            }
        }
        private ButtonMap _gb = null;

        public ButtonMap MouseAndKeyboardSettings
        {
            get
            {
                return _kb ?? (_kb = ButtonMap.FromSerializedForm(_mouseAndKeyboardSettings));
            }

            set
            {
                _kb = value;
            }
        }
        private ButtonMap _kb = null;

        [SerializeField] public ButtonMap.SerializedForm _gamepadSettings;
        [SerializeField] public ButtonMap.SerializedForm _mouseAndKeyboardSettings;
        [SerializeField] public float SFXVolume = .1f;
        [SerializeField] public float MusicVolume = .1f;

        public void BeforeSerialization()
        {
            _gamepadSettings = new ButtonMap.SerializedForm(_gb);
            _mouseAndKeyboardSettings = new ButtonMap.SerializedForm(_kb);
        }

        public void AfterDeSerialization()
        {
            _kb = ButtonMap.FromSerializedForm(_mouseAndKeyboardSettings);
            _gb = ButtonMap.FromSerializedForm(_gamepadSettings);
        }
    }
}
