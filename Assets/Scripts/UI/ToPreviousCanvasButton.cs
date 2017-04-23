using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.ui
{

    public class ToPreviousCanvasButton : BasicMenuButton
    {
        private MenuCanvas _parentMenu;

        void Start()
        {
            _parentMenu = GetComponentInParent<MenuCanvas>();
        }

        public override Action ClickThis(MenuCommander commander)
        {
            if (_parentMenu != null) _parentMenu.ReturnToPreviousCanvas(commander);
            return null;
        }

        public override void Submit(MenuCommander commander)
        {
            if (_parentMenu != null) _parentMenu.ReturnToPreviousCanvas(commander);
        }

        public override void Activate()
        {
        }
    }
}
