using System;
using System.Text.RegularExpressions;
using fi.tamk.hellgame.interfaces;
using tamk.fi.hellgame.character;
using UnityEngine;

namespace fi.tamk.hellgame.input
{
    /// <summary>
    /// Manages player inputs. Poll this instead of UnityEngine.Input.
    /// </summary>
    public class InputController : MonoBehaviour, IInput
    {
        /// <summary>
        /// Setting new config automatically re-initializes this controller and
        /// rebinds keys associated with this controller.
        /// </summary>
        public ButtonMap MyConfig
        {
            get { return _myConfig; }
            set
            {
                _myConfig = value;
                Initialize();
            }
        }

        /// <summary>
        /// Backing field  for MyConfig.
        /// </summary>
        [SerializeField] private ButtonMap _myConfig;

        /// <summary>
        /// Stores combined name for an axis. This depends on which Joypad/keyboard is assigned to this
        /// InputController.
        /// </summary>
        private string _horizontalAxisLeft;

        /// <summary>
        /// Stores combined name for an axis. This depends on which Joypad/keyboard is assigned to this
        /// InputController.
        /// </summary>
        private string _vercticalAxisLeft;

        private string _leftTriggerAxis;

        /// <summary>
        /// Stores combined name for an axis. This depends on which Joypad/keyboard is assigned to this
        /// InputController.
        /// </summary>
        private string _horizontalAxisRight;

        /// <summary>
        /// Stores combined name for an axis. This depends on which Joypad/keyboard is assigned to this
        /// InputController.
        /// </summary>
        private string _verticalAxisRight;

        /// <summary>
        /// Stores assigned Joypad number of this InputController. 0 is for glorius PC Masterrace. 1-14
        /// for dirty console plebs. (=joypads.)
        /// </summary>
        private int _joyNumber;

        /// <summary>
        /// Polls movement "stick" for combined axis.
        /// </summary>
        /// <returns>Combined axis of horizontal and vertical axis</returns>
        public virtual Vector3 PollAxisLeft()
        {
            return PollStickByStrings(_horizontalAxisLeft, _vercticalAxisLeft);
        }

        /// <summary>
        /// Polls aiming "stick" for combined axis.
        /// </summary>
        /// <returns>Combined axis of horisontal and vertical axis</returns>
        public virtual Vector3 PollAxisRight()
        {
            return _myConfig.InputType != Buttons.InputType.PcMasterrace
                ? PollStickByStrings(_horizontalAxisRight, _verticalAxisRight) : MouseAim();
        }

        /// <summary>
        /// Polls if this controller detects given button held down during this frame.
        /// </summary>
        /// <param name="whichButton">which bindable button to check for</param>
        /// <returns>if the button is held down</returns>
        public virtual bool PollButton(Buttons.ButtonScheme whichButton)
        {
            if (_myConfig.InputType != Buttons.InputType.PcMasterrace) return Input.GetKey(ButtonToKeyCode(whichButton));
            return Input.GetKey(ButtonToKeyCode(whichButton));
        }

        /// <summary>
        /// Polls if this controller detects given button held just pressed down during this frame.
        /// </summary>
        /// <param name="whichButton">which bindable button to check for</param>
        /// <returns>if the button just pressed down</returns>
        public virtual bool PollButtonDown(Buttons.ButtonScheme whichButton)
        {
            if (_myConfig.InputType != Buttons.InputType.PcMasterrace) return Input.GetKeyDown(ButtonToKeyCode(whichButton));
            return Input.GetKeyDown(ButtonToKeyCode(whichButton));
        }

        /// <summary>
        /// Polls if this controller detects given button released during this frame.
        /// </summary>
        /// <param name="whichButton">which bindable button to check for</param>
        /// <returns>if the button is just released</returns>
        public virtual bool PollButtonUp(Buttons.ButtonScheme whichButton)
        {
            if (_myConfig.InputType != Buttons.InputType.PcMasterrace) return Input.GetKeyUp(ButtonToKeyCode(whichButton));
            return Input.GetKeyUp(ButtonToKeyCode(whichButton));
        }

        public virtual float PollLeftTrigger()
        {
            return Mathf.Abs(_myConfig.InputType == Buttons.InputType.PcMasterrace ? (Input.GetKey(_myConfig.LeftTrigger) ? 1f : 0f) : PollTrigger(_leftTriggerAxis));
        }

        /// <summary>
        /// Reserves a joypad number and caches some axis names based on that string.
        /// </summary>
        public void Initialize()
        {
            _joyNumber = GetJoypadNumber(this);

            _horizontalAxisLeft = GetCombinedInputName("Horizontal_Left");
            _vercticalAxisLeft = GetCombinedInputName("Vertical_Left");
            _horizontalAxisRight = GetCombinedInputName("Horizontal_Right");
            _verticalAxisRight = GetCombinedInputName("Vertical_Right");
            _leftTriggerAxis = GetCombinedInputName("Trigger_Left");
        }

        /// <summary>
        /// Mouse aim right stick for pc masterrace.
        /// </summary>
        /// <returns>Combined axis vector</returns>
        private Vector3 MouseAim()
        {
            var rawMouse = MouseLookUp.Instance.GetMousePosition();
            var vec = rawMouse - gameObject.transform.position;
            return vec.sqrMagnitude > 1f ? vec.normalized : vec;
        }

        /// <summary>
        /// Reserves a joypad number for given InputController. If that InputController already has reserved
        /// a controller (might happen in case of rebinding of keys), same reservation remains.
        /// </summary>
        /// <param name="whichController">which InputController to reserve the pad for</param>
        /// <returns>number of pad reserved for that InputController. O is reserved for Keyboard, 1-14 for
        /// Joypads. -1 is an error.</returns>
        private static int GetJoypadNumber(InputController whichController)
        {
            return whichController.MyConfig.InputType == Buttons.InputType.PcMasterrace ? 0 : 1;
        }

        /// <summary>
        /// Combines axis name string with joypad number to get actual axis name used by unity manager.
        /// </summary>
        /// <param name="rawAxisName">which axis name to convert</param>
        /// <returns>combined axis name</returns>
        private string GetCombinedInputName(string rawAxisName)
        {
            return string.Format("{0}_{1}", rawAxisName, _joyNumber);
        }

        /// <summary>
        /// Polls Unitys Input manager for axis'
        /// </summary>
        /// <param name="whichHorizontal"></param>
        /// <param name="whichVertical"></param>
        /// <returns></returns>
        private Vector3 PollStickByStrings(string whichHorizontal, string whichVertical)
        {
            var vec = new Vector3(Input.GetAxis(whichHorizontal), 0, Input.GetAxis(whichVertical));
            return vec.sqrMagnitude < 0.09f ? Vector3.zero : vec; // sqrMagnitude is a magnitude cheaper than magnitude :)
        }

        private float PollTrigger(string whichTrigger)
        {
            return (Input.GetAxis(whichTrigger));
        }

        public KeyCode ButtonToKeyCode(Buttons.ButtonScheme button)
        {
            switch (button)
            {
                case Buttons.ButtonScheme.Fire_1:
                    return _myConfig.FireOneButton;
                case Buttons.ButtonScheme.Fire_2:
                    return _myConfig.FireTwoButton;
                case Buttons.ButtonScheme.Dash:
                    return _myConfig.DashButton;
                case Buttons.ButtonScheme.LimitBreak:
                    return _myConfig.LimitBreakButton;
                case Buttons.ButtonScheme.Pause:
                    return _myConfig.PauseButton;
                default:
                    throw new ArgumentOutOfRangeException("button", button, null);
            }
        }

        /// <summary>
        /// Initializes the config given in editor.
        /// </summary>
        private void Awake()
        {
            Initialize();
        }

        /// <summary>
        /// Hacky way of passing mouse presses as keycodes
        /// </summary>
        /// <param name="buttonCode"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        private bool GetMouseButtonCode(ref int buttonCode, string input)
        {
            var btnStr = Regex.Split(input, "_");
            return btnStr.Length == 2 && int.TryParse(btnStr[1], out buttonCode);
        }

        /// <summary>
        /// Formats defined string into a keycode into what can be passed into Input.GetKey(..)
        /// </summary>
        /// <param name="rawKeyCode">which code</param>
        /// <returns>converted code</returns>
        private string GetFormattedJoySticKeyCode(string rawKeyCode)
        {
            return string.Format("joystick {0} button {1}", _joyNumber, rawKeyCode);
        }
    }
}