using fi.tamk.hellgame.dataholders;
using fi.tamk.hellgame.input;
using fi.tamk.hellgame.utils;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace fi.tamk.hellgame.ui
{

    public class WorldSpaceButtonPromt : MonoBehaviour
    {
        private Buttons.InputType _inputType = Buttons.InputType.PcMasterrace;
        [SerializeField] private Buttons.ButtonScheme _buttonToShow;
        [SerializeField] private TextMeshPro _promtText;
        [SerializeField] private SpriteRenderer _buttonSprite;
        [SerializeField] private ButtonPromtData _buttonPromtData;

        private void Start()
        {
            var JoySticks = Input.GetJoystickNames();

            if (JoySticks.Length > 0)
            {
                _inputType = Buttons.InputType.ConsolePleb;
            }

            ButtonMap buttonMap = null;

            if (_inputType == Buttons.InputType.ConsolePleb)
            {
                buttonMap = UserStaticData.GetGameSettings().GamepadSettings;
            } else
            {
                buttonMap = UserStaticData.GetGameSettings().MouseAndKeyboardSettings;
            }

            if (buttonMap == null) return;

            var keycode = Utilities.ReturnKeyCodeFromButtonMap(_buttonToShow, buttonMap);

            var buttonData = _buttonPromtData.GetButtonPromtData(keycode);

            if (buttonData == null)
            {
                _promtText.text = keycode.ToString();
                _buttonSprite.enabled = false;
            } else
            {
                _buttonSprite.sprite = buttonData._promtTexture;
                _promtText.text = "";
            }
        }
    }
}
