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
        [Serializable]
        public struct SerializedForm
        {
            [SerializeField] public KeyCode FireOneButton;
            [SerializeField] public KeyCode FireTwoButton;
            [SerializeField] public KeyCode DashButton;
            [SerializeField] public string LeftTrigger;
            [SerializeField] public KeyCode LimitBreakButton;
            [SerializeField] public KeyCode PauseButton;

            public SerializedForm(ButtonMap copy)
            {
                FireOneButton = copy.FireOneButton;
                FireTwoButton = copy.FireTwoButton;
                DashButton = copy.DashButton;
                LeftTrigger = copy.LeftTrigger;
                LimitBreakButton = copy.LimitBreakButton;
                PauseButton = copy.PauseButton;
            }
        }

        [SerializeField] public KeyCode FireOneButton;
        [SerializeField] public KeyCode FireTwoButton;
        [SerializeField] public KeyCode DashButton;
        [SerializeField] public string LeftTrigger;
        [SerializeField] public KeyCode LimitBreakButton;
        [SerializeField] public KeyCode PauseButton;

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

        public SerializedForm GetSerializedForm()
        {
            var rv = new SerializedForm()
            {
                FireOneButton = FireOneButton,
                FireTwoButton = FireTwoButton,
                DashButton = DashButton,
                LeftTrigger = LeftTrigger,
                LimitBreakButton = LimitBreakButton,
                PauseButton = PauseButton
            };
            return rv;
        }

        public static ButtonMap FromSerializedForm(SerializedForm serialized)
        {
            var rv = ScriptableObject.CreateInstance<ButtonMap>();
            rv.FireOneButton = serialized.FireOneButton;
            rv.FireTwoButton = serialized.FireTwoButton;
            rv.DashButton = serialized.DashButton;
            rv.LeftTrigger = serialized.LeftTrigger;
            rv.LimitBreakButton = serialized.LimitBreakButton;
            rv.PauseButton = serialized.PauseButton;
            return rv;
        }
    }
}