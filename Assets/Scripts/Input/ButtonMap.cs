using System;
using UnityEngine;

namespace fi.tamk.hellgame.input
{
    [Serializable]
    /// <summary>
    /// Settings container for input controller for easy reconfiguration and configuration storing etc crap.
    /// </summary>
    public class ButtonMap : ScriptableObject
    {
        public KeyCode FireOneButton;
        public KeyCode FireTwoButton;
        public KeyCode DashButton;
        public string LeftTrigger;
        public KeyCode LimitBreakButton;
        public KeyCode PauseButton;

        public Buttons.InputType InputType;

        public KeyCode GetKeyCodeAttachedToButton(Buttons.ButtonScheme _command)
        {
            switch (_command)
            {
                case Buttons.ButtonScheme.Dash:
                    return DashButton;
                    break;
                case Buttons.ButtonScheme.LimitBreak:
                    return LimitBreakButton;
                    break;
                case Buttons.ButtonScheme.Pause:
                    return PauseButton;
                    break;
            }

            return KeyCode.None;
        }
    }
}