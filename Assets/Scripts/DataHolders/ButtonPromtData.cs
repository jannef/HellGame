using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.dataholders
{
    [Serializable]
    public class ButtonPromtTextureReference
    {
        public Sprite _promtTexture;
        public int _spaceAmount = 2;
        public KeyCode _keyCode;
    }

    public class ButtonPromtData : ScriptableObject
    {
        public List<ButtonPromtTextureReference> _buttonPromtData;

        public ButtonPromtTextureReference GetButtonPromtData(KeyCode code)
        {
            return _buttonPromtData.FirstOrDefault( x => x._keyCode == code);
        }
        
    }
}
