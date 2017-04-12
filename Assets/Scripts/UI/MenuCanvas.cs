using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace fi.tamk.hellgame.ui
{

    public class MenuCanvas : InteractableUiElementAbstract
    {
        [SerializeField] private InteractableUiElementAbstract _canvasToReturnTo;
        [SerializeField] public InteractableUiElementAbstract _startButton;
        private CanvasGroup _canvasGroup;
        public UnityEvent PointerMovedToThis;
        public UnityEvent PointerRemovedFromThis;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void OpenThisMenu()
        {
            var Menucommander = GetComponentInParent<MenuCommander>();
            Menucommander.Activate(this);
        }

        public override void MovePointerFromThis()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.blocksRaycasts = false;
            gameObject.SetActive(false);
            if (PointerRemovedFromThis != null) PointerRemovedFromThis.Invoke();
        }

        public override void ClickThis(MenuCommander commander)
        {
            
        }

        public virtual void ReturnToPreviousCanvas(MenuCommander commander)
        {
            MovePointerFromThis();
            _canvasToReturnTo.MovePointerToThis(commander);
        }

        public override void MovePointerToThis(MenuCommander commander)
        {
            _canvasGroup.blocksRaycasts = true;
            gameObject.SetActive(true);
            _startButton.MovePointerToThis(commander);

            if (PointerMovedToThis != null) PointerMovedToThis.Invoke();

            if (_canvasToReturnTo != null)
            {
                commander.AddCommand(MenuActionType.Cancel, ReturnToPreviousCanvas);
            }
        }
    }
}
