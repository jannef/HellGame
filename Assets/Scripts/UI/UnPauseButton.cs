using fi.tamk.hellgame.world;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace fi.tamk.hellgame.ui
{

    public class UnPauseButton : BasicMenuButton
    {
        public override void Submit(MenuCommander commander)
        {
            RoomIdentifier.PauseGame();
            base.Submit(commander);
        }

        public override Action ClickThis(MenuCommander commander)
        {
            RoomIdentifier.PauseGame();
            return base.ClickThis(commander);
        }

        public override void Activate()
        {
            base.Activate();
            
        }
    }
}
