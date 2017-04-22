using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.ui
{

    public class QuitGameUiButton : BasicMenuButton
    {

        public override void Submit(MenuCommander commander)
        {
            base.Submit(commander);
            Application.Quit();
        }

        public override Action ClickThis(MenuCommander commander)
        {
            Submit(commander);
            return null;
        }
    }
}
