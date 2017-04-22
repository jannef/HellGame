using fi.tamk.hellgame.ui;
using fi.tamk.hellgame.utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.ui
{

    public class CanvasTransitionMenuButton : BasicMenuButton
    {
        [SerializeField] private MenuCanvas _destinationCanvas;
        private MenuCanvas _parentCanvas;

        private void Start()
        {
            _parentCanvas = GetComponentInParent<MenuCanvas>();
        }

        public override Action ClickThis(MenuCommander commander)
        {
            Submit(commander);
            return null;

        }

        public override void Submit(MenuCommander commander)
        {
            Utilities.PlayOneShotSound(SoundEventReferences.SubmitSoundEvent, transform.position);
            _parentCanvas._startButton = this;
            _parentCanvas.MovePointerFromThis();
            _destinationCanvas.MovePointerToHisWithoutSound(commander);
        }
    }
}
