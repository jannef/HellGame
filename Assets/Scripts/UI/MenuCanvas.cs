using fi.tamk.hellgame.dataholders;
using fi.tamk.hellgame.utils;
using fi.tamk.hellgame.world;
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
        public UIButtonSoundEventReferences Sounds;
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
            GameResumed();
            if (PointerRemovedFromThis != null) PointerRemovedFromThis.Invoke();
        }

        void GameResumed()
        {
            RoomIdentifier.GameResumed -= GameResumed;
            _canvasGroup.alpha = 0;
            _canvasGroup.blocksRaycasts = false;
        }

        public virtual void ReturnToPreviousCanvas(MenuCommander commander)
        {
            commander.RemoveCommand(MenuActionType.Cancel);
            MovePointerFromThis();
            if (_canvasToReturnTo != null && Sounds != null) Utilities.PlayOneShotSound(Sounds.CancelSoundEvent, transform.position);
            _canvasToReturnTo.MovePointerToThis(commander);
        }

        public override void MovePointerToThis(MenuCommander commander)
        {
            RoomIdentifier.GameResumed += GameResumed;
            gameObject.SetActive(true);
            _startButton.MovePointerToHisWithoutSound(commander);
            if (_canvasGroup == null)
            {
                _canvasGroup = GetComponent<CanvasGroup>();
                Debug.Log("CanvasGroup in MenuCanvas is null for some reason");
            }

            _canvasGroup.blocksRaycasts = true;

            if (PointerMovedToThis != null) PointerMovedToThis.Invoke();

            if (_canvasToReturnTo != null)
            {
                commander.AddCommand(MenuActionType.Cancel, ReturnToPreviousCanvas);
            }
        }

        public override Action ClickThis(MenuCommander commander)
        {
            return null;
        }

        public override void MovePointerToHisWithoutSound(MenuCommander commander)
        {
            MovePointerToThis(commander);
        }
    }
}
