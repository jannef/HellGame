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
        [SerializeField] public KeyCode FireOneButton;
        [SerializeField]
        public KeyCode FireTwoButton;
        [SerializeField]
        public KeyCode DashButton;
        [SerializeField]
        public string LeftTrigger;
        [SerializeField]
        public KeyCode LimitBreakButton;
        [SerializeField]
        public KeyCode PauseButton;

        [SerializeField] public Buttons.InputType InputType;

        public KeyCode GetKeyCodeAttachedToButton(Buttons.ButtonScheme _command)
        {
            switch (_command)
            {
                case Buttons.ButtonScheme.Dash:
                    return DashButton;
                case Buttons.ButtonScheme.LimitBreak:
                    return LimitBreakButton;
                case Buttons.ButtonScheme.Pause:
                    return PauseButton;
            }

            return KeyCode.None;
        }
    }
}