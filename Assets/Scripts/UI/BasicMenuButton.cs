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
            Debug.Log(gameObject.name);
            foreach (MenuActionType type in Constants.ActionsToAlwaysSetOnBasicButtons)
            {
                var transition = _transitionList.Where(x => x._actionType == type).FirstOrDefault();
                

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
            
            commander.AddCommand(MenuActionType.Submit, Submit);
            _button.GetComponentInChildren<Text>().text = "chosen";
            commander.currentlySelectedButton = this;
        }

        public override void MovePointerFromThis()
        {
            _button.GetComponentInChildren<Text>().text = "not chosen";
        }

        private void Submit(MenuCommander commander)
        {
            Activate();
            _button.onClick.Invoke();
        }

        protected virtual void Activate()
        {
            Debug.Log(gameObject.name);
        }

        // Use this for initialization
        void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(Activate);
        }
    }
}
