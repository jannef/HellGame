using fi.tamk.hellgame.dataholders;
using System;
using System.Collections;
using System.Collections.Generic;
using tamk.fi.hellgame.character;
using UnityEngine;

namespace fi.tamk.hellgame.input
{

    public class MultipleInputReader : InputController
    {
        [SerializeField] public InputController GamePadInput;
        [SerializeField] public InputController KeyBoardInput;
        [SerializeField] private float MouseAttentionGrabThreshold;
        private Vector3 _lastMousePosition;

        private bool HasMovedKeyPadRight
        {
            get
            {
                return _hasMovedKeypadRight;
            }
            set
            {
                if (_hasMovedKeypadRight != value)
                {
                    _hasMovedKeypadRight = value;
                    if (primaryInputChanged != null) primaryInputChanged.Invoke(_hasMovedKeypadRight);
                }
            }
        }

        private bool _hasMovedKeypadRight = false;
        public static event Action<bool> primaryInputChanged;

        public void Start()
        {
            var Settings = UserStaticData.GetGameSettings();

            if (Settings != null)
            {
                GamePadInput.MyConfig = Settings.GamepadSettings;
                KeyBoardInput.MyConfig = Settings.MouseAndKeyboardSettings;
            }
        }

        public override Vector3 PollAxisRight()
        {
            var gamepadVector = GamePadInput.PollAxisRight();

            if (gamepadVector.sqrMagnitude > 0.1)
            {
                HasMovedKeyPadRight = true;
                Cursor.visible = false;
                _lastMousePosition = Input.mousePosition;
                return gamepadVector;
            } else
            {
                if (HasMovedKeyPadRight)
                {
                    var thisMousePosition = Input.mousePosition;
                    if ((_lastMousePosition - thisMousePosition).sqrMagnitude > MouseAttentionGrabThreshold)
                    {
                        Cursor.visible = true;
                        HasMovedKeyPadRight = false;
                    }

                    return Vector3.zero;
                } else
                {
                    return KeyBoardInput.PollAxisRight();
                }
            }
        }

        public override Vector3 PollAxisLeft()
        {
            var gamepadVector = GamePadInput.PollAxisLeft();

            if (gamepadVector.sqrMagnitude > 0.01)
            {
                return gamepadVector;
            }

            return KeyBoardInput.PollAxisLeft();
        }

        public override bool PollButtonDown(Buttons.ButtonScheme button)
        {
            if (GamePadInput.PollButtonDown(button))
            {
                return true;
            } else
            {
                return KeyBoardInput.PollButtonDown(button);
            }
        }

        public override bool PollButtonUp(Buttons.ButtonScheme button)
        {
            if (GamePadInput.PollButtonUp(button))
            {
                return true;
            }
            else
            {
                return KeyBoardInput.PollButtonUp(button);
            }
        }

        public override bool PollButton(Buttons.ButtonScheme button)
        {

            if (GamePadInput.PollAxisRight().sqrMagnitude > 0.01)
            {
                return true;
            }
            else
            {
                return KeyBoardInput.PollButton(button);
            }
        }

        private void OnDestroy()
        {
            primaryInputChanged = null;
        }
    }
}
