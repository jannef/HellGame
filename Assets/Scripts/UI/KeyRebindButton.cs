using fi.tamk.hellgame.input;
using fi.tamk.hellgame.utils;
using fi.tamk.hellgame.world;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace fi.tamk.hellgame.ui
{

    public class KeyRebindButton : BasicMenuButton
    {
        [SerializeField] private Buttons.ButtonScheme _buttonToRebind;
        [SerializeField] private Buttons.InputType _inputType;
        [SerializeField] private ButtonGraphic _buttonGraphic;
        [SerializeField] private Text _pressAnyButtonText;
        private ButtonMap _buttonMapToChange;

        void Start()
        {
            _buttonMapToChange = ServiceLocator.Instance.RoomManagerReference.GetControllerBasedOnInputType(_inputType);
            UpdateButtonGraphic(_buttonMapToChange.GetKeyCodeAttachedToButton(_buttonToRebind));
        }

        public override void ClickThis(MenuCommander commander)
        {
            _buttonGraphic.Disappear();
            _pressAnyButtonText.enabled = true;
            StartCoroutine(WaitForInputCoroutine(commander.DisableInputReading()));
        }

        private void UpdateButtonGraphic(KeyCode input)
        {
            _buttonGraphic.enabled = true;
            _pressAnyButtonText.enabled = false;
            _buttonGraphic.UpdateGraphics(input);
        }

        private void ChangeInput(KeyCode newInput)
        {

            switch (_buttonToRebind)
            {
                case Buttons.ButtonScheme.Dash:
                    _buttonMapToChange.DashButton = newInput;
                    break;
                case Buttons.ButtonScheme.LimitBreak:
                    _buttonMapToChange.LimitBreakButton = newInput;
                    break;
                case Buttons.ButtonScheme.Pause:
                    _buttonMapToChange.PauseButton = newInput;
                    break;
            }

            UpdateButtonGraphic(newInput);
        }

        private KeyCode FetchPressedKey()
        {

            var e = 400;

            for (var i = 0; i < e; i++)
            {
                    KeyCode key = (KeyCode)i;
                    if (Input.GetKeyDown(key))
                    {

                        return key;
                    }
            }
            return KeyCode.None;
        }

        private IEnumerator WaitForInputCoroutine(Action callBack)
        {
            yield return new WaitForEndOfFrame();

            while (true)
            {
                if (Input.anyKeyDown)
                {

                    KeyCode reboundKey = FetchPressedKey();

                    if (reboundKey == KeyCode.None)
                    {

                        if (Input.GetKeyDown(KeyCode.LeftAlt)) { reboundKey = KeyCode.LeftAlt; }
                        if (Input.GetKeyDown(KeyCode.RightAlt)) { reboundKey = KeyCode.RightAlt; }
                        if (Input.GetKeyDown(KeyCode.LeftShift)) { reboundKey = KeyCode.LeftShift; }
                        if (Input.GetKeyDown(KeyCode.RightShift)) { reboundKey = KeyCode.RightShift; }
                        if (Input.GetKeyDown(KeyCode.LeftControl)) { reboundKey = KeyCode.LeftControl; }
                        if (Input.GetKeyDown(KeyCode.RightControl)) { reboundKey = KeyCode.RightControl; }
                    }

                    if (reboundKey != KeyCode.None)
                    {
                        callBack.Invoke();
                        ChangeInput(reboundKey);
                        break;
                    }
                }

                yield return null;
            }

            
        }
    }
}
