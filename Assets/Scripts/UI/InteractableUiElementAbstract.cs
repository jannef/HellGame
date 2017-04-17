using fi.tamk.hellgame.input;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.ui
{

    public abstract class InteractableUiElementAbstract : MonoBehaviour {

        public abstract void MovePointerToThis(MenuCommander commander);
        public abstract void MovePointerFromThis();
        public abstract Action ClickThis(MenuCommander commander);
    }
}
