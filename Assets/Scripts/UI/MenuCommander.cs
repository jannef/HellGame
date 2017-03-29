using fi.tamk.hellgame.input;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.ui
{
    public enum MenuActionType
    {
        Up, Down, Left, Right, Submit, Cancel
    }

    public class MenuCommander : MonoBehaviour
    {
        public InputController _input;
        private Dictionary<MenuActionType, Action<MenuCommander>> _commands = new Dictionary<MenuActionType, Action<MenuCommander>>();
        public InteractableUiElementAbstract currentlySelectedButton;
        [SerializeField] private InteractableUiElementAbstract TestStartButton;
        private float _movementlock;

        public void Activate(InteractableUiElementAbstract startButton)
        {
            startButton.MovePointerToThis(this);
            this.enabled = true;
        }

        private void Start()
        {
            Activate(TestStartButton);
        }

        private void DoCommand(MenuActionType type)
        {
            _movementlock = 0.25f;

            if (_commands.ContainsKey(type))
            {
                _commands[type].Invoke(this);
            }
        }

        public void AddCommand(MenuActionType type, Action<MenuCommander> command)
        {
            if (_commands.ContainsKey(type))
            {
                _commands.Remove(type);
            }

            _commands.Add(type, command);
        }

        public void RemoveCommand(MenuActionType type)
        {
            if (_commands.ContainsKey(type))
            {
                _commands.Remove(type);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (_movementlock > 0)
            {
                _movementlock -= Time.unscaledDeltaTime;
                return;
            }
            Vector3 inputAxis = _input.PollAxisLeft();

            if (Math.Abs(inputAxis.z) > .8)
            {
                if (inputAxis.z < 0)
                {
                    DoCommand(MenuActionType.Down);
                    return;
                } else
                {
                    DoCommand(MenuActionType.Up);
                    return;
                }
            } else if (Math.Abs(inputAxis.x) > .8)
            {
                if (inputAxis.x < 0)
                {
                    DoCommand(MenuActionType.Left);
                    return;
                }
                else
                {
                    DoCommand(MenuActionType.Right);
                    return;
                }
            }

            if (_input.PollButtonDown(Buttons.ButtonScheme.LimitBreak))
            {
                DoCommand(MenuActionType.Submit);
                return;
            }

            if (_input.PollButtonDown(Buttons.ButtonScheme.Dash))
            {
                DoCommand(MenuActionType.Cancel);
                return;
            }
        }
    }
}
