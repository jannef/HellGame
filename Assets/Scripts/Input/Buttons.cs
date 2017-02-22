using System;
using UnityEngine;

namespace fi.tamk.hellgame.input
{
    /// <summary>
    /// Holds Input related small Data-types.
    /// </summary>
    public static class Buttons
    {
        /// <summary>
        /// List of mappable buttons. Analog axises aren't mapped, but instead they have to be digged
        /// manually from unitys horrible input manager :(
        /// </summary>
        public enum ButtonScheme
        {
            Fire_1 = 0,
            Fire_2 = 1,
            Dash = 2,
            LimitBreak = 3
        }

        /// <summary>
        /// Different types on input. ConsolePleb means joypad, PcMasterrace is for Mouse and Keyboard.
        /// </summary>
        public enum InputType
        {
            PcMasterrace    = 0,
            ConsolePleb     = 1,
            Ai              = 2
        }
    }
}