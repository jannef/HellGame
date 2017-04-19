using fi.tamk.hellgame.utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Linq;

namespace fi.tamk.hellgame.ui
{
    [Serializable]
    public class ButtonTransitionAction
    {
        public MenuActionType _actionType;
        public InteractableUiElementAbstract _buttonToChoose;
    }

    public class BasicMenuButton : InteractableUiElementAbstract
    {
        [SerializeField] private List<ButtonTransitionAction> _transitionList;
        private Button _button;

        public override void MovePointerToThis(MenuCommander commander)
        {
            foreach (MenuActionType type in Constants.ActionsToAlwaysSetOnBasicButtons)
            {
                var transition = _transitionList.FirstOrDefault(x => x._actionType == type);

                if (transition != null)
                {
                    commander.AddCommand(type, transition._buttonToChoose.MovePointerToThis);
                } else
                {
                    commander.RemoveCommand(type);
                }
            }

            if (commander.currentlySelectedButton != null)
            {
                commander.currentlySelectedButton.MovePointerFromThis();
            }
            AddSubmitCommand(commander);

            commander.currentlySelectedButton = this;
            var rectTranform = GetComponent<RectTransform>();
            if (rectTranform != null) commander.SetPointer(GetComponent<RectTransform>());
        }

        protected virtual void AddSubmitCommand(MenuCommander commander)
        {
            commander.AddCommand(MenuActionType.Submit, Submit);
        }

        public override void MovePointerFromThis()
        {
        }

        public override Action ClickThis(MenuCommander commander)
        {
            Activate();
            _button.onClick.Invoke();
            return null;
        }

        public virtual void Submit(MenuCommander commander)
        {
            Activate();
            _button.onClick.Invoke();
        }

        public virtual void Activate()
        {
        }

        // Use this for initialization
        void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(Activate);
        }
    }
}
