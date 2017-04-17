using fi.tamk.hellgame.input;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace fi.tamk.hellgame.ui
{
    public enum MenuActionType
    {
        Up, Down, Left, Right, Submit, Cancel
    }

    public struct MenuMouseStatus
    {
        public Vector3 _mousePosition;
        public bool _isActive;
        public float _activeTimer;

        public MenuMouseStatus(Vector3 mousePosition, bool v) : this()
        {
            this._mousePosition = mousePosition;
            this._isActive = v;
            _activeTimer = 0f;
        }

        public void ResetActiveTimer(Vector2 mousePosition)
        {
            _activeTimer = .15f;
            _isActive = true;
            _mousePosition = mousePosition;
        }
    }

    public class MenuCommander : MonoBehaviour
    {
        public InputController _input;
        private MenuMouseStatus _mouseStatus;
        private Dictionary<MenuActionType, Action<MenuCommander>> _commands = new Dictionary<MenuActionType, Action<MenuCommander>>();
        public InteractableUiElementAbstract currentlySelectedButton;
        [SerializeField] private InteractableUiElementAbstract TestStartButton;
        [SerializeField] private LayerMask _uiMask;
        private float _movementlock;
        private bool _isAcceptingInput = true;
        private GraphicRaycaster _graphicRaycaster;

        public void Activate(InteractableUiElementAbstract startButton)
        {
            startButton.MovePointerToThis(this);
            this.enabled = true;
        }

        private void Start()
        {
            _mouseStatus = new MenuMouseStatus(Input.mousePosition, false);
            _graphicRaycaster = GetComponent<GraphicRaycaster>();
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

        public Action DisableInputReading()
        {
            _isAcceptingInput = false;

            return EnableThis;
        }

        private void EnableThis()
        {
            _isAcceptingInput = true;
        }

        public void RemoveCommand(MenuActionType type)
        {
            if (_commands.ContainsKey(type))
            {
                _commands.Remove(type);
            }
        }

        private bool CheckForMovementInput()
        {
            Vector3 inputAxis = _input.PollAxisLeft();

            if (Math.Abs(inputAxis.x) > .8)
            {
                if (inputAxis.x < 0)
                {
                    DoCommand(MenuActionType.Left);
                    return true;
                }
                else
                {
                    DoCommand(MenuActionType.Right);
                    return true;
                }
            }

            if (_movementlock > 0)
            {
                _movementlock -= Time.unscaledDeltaTime;
                return false;
            }

            if (Math.Abs(inputAxis.z) > .8)
            {
                if (inputAxis.z < 0)
                {
                    DoCommand(MenuActionType.Down);
                    return true;
                }
                else
                {
                    DoCommand(MenuActionType.Up);
                    return true;
                }
            }
            else if (Math.Abs(inputAxis.x) > .8)
            {
                if (inputAxis.x < 0)
                {
                    DoCommand(MenuActionType.Left);
                    return true;
                }
                else
                {
                    DoCommand(MenuActionType.Right);
                    return true;
                }
            }

            return false;
        }

        private InteractableUiElementAbstract MouseMovementUpdate(Vector2 mousePosition, bool ignoreCurrentChosenButton = false)
        {
            PointerEventData ped = new PointerEventData(null);
            ped.position = mousePosition;
            //Create list to receive all results
            List<RaycastResult> results = new List<RaycastResult>();
            //Raycast it
            _graphicRaycaster.Raycast(ped, results);
            foreach (RaycastResult result in results)
            {
                if (ignoreCurrentChosenButton || result.gameObject.GetInstanceID() != currentlySelectedButton.gameObject.GetInstanceID())
                {
                    var interactableObject = result.gameObject.GetComponent<InteractableUiElementAbstract>();
                    if (interactableObject != null) return interactableObject;
                }
                
            }

            return null;
        }

        private void CheckMouseInput()
        {
            var mousePosition = Input.mousePosition;

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                var result = MouseMovementUpdate(mousePosition, true);
                if (result != null)
                {
                    result.ClickThis(this);
                }

                return;
            }

            if (_mouseStatus._isActive)
            {
                _mouseStatus._activeTimer -= Time.unscaledDeltaTime;
                if (_mouseStatus._activeTimer <= 0)
                {
                    if ((mousePosition - _mouseStatus._mousePosition).sqrMagnitude > .1f)
                    {
                        _mouseStatus.ResetActiveTimer(mousePosition);
                    } else
                    {
                        _mouseStatus._isActive = false;
                    }
                }

                var objectUnderMouse = MouseMovementUpdate(mousePosition);
                if (objectUnderMouse != null)
                {
                    objectUnderMouse.MovePointerToThis(this);
                }             
            } else
            {
                if ((mousePosition - _mouseStatus._mousePosition).sqrMagnitude > .1f)
                {
                    _mouseStatus.ResetActiveTimer(mousePosition);
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            CheckMouseInput();

            if (!_isAcceptingInput) return;

            if (CheckForMovementInput()) return;

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
