using UnityEngine;

namespace fi.tamk.hellgame.input
{
    /// <summary>
    /// Settings container for input controller for easy reconfiguration and configuration storing etc crap.
    /// </summary>
    public class ButtonMap : ScriptableObject
    {
        public string FireOneButton;
        public string FireTwoButton;
        public string DashButton;
        public string LeftTrigger;
        public string LimitBreakButton;
        public string PauseButton;

        public Buttons.InputType InputType;
    }
}