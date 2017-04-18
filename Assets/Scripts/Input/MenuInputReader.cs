using System.Collections;
using System.Collections.Generic;
using tamk.fi.hellgame.character;
using UnityEngine;

namespace fi.tamk.hellgame.input
{

    public class MenuInputReader : InputController
    {
        [SerializeField] public InputController[] _inputControllers;
        [SerializeField] private float _axisDeadZone = 0.01f;

        public override Vector3 PollAxisLeft()
        {
            foreach (InputController input in _inputControllers)
            {
                var axisLeft = input.PollAxisLeft();
                if (axisLeft.sqrMagnitude > _axisDeadZone)
                {
                    return axisLeft;
                }
            }

            return Vector3.zero;
        }

        public override bool PollButtonDown(Buttons.ButtonScheme button)
        {
            foreach (InputController input in _inputControllers)
            {
                if (input.PollButtonDown(button)) return true;
            }

            return false;
        }

        public override bool PollButtonUp(Buttons.ButtonScheme button)
        {
            foreach (InputController input in _inputControllers)
            {
                if (input.PollButtonUp(button)) return true;
            }

            return false;
        }

        public override bool PollButton(Buttons.ButtonScheme button)
        {
            foreach (InputController input in _inputControllers)
            {
                if (input.PollButton(button)) return true;
            }

            return false;
        }
    }
}
