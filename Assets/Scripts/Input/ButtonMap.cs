using UnityEngine;

namespace fi.tamk.hellgame.input
{
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
    }
}