using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace fi.tamk.hellgame.ui
{

    public class UnityEventUIButton : BasicMenuButton {
        public UnityEvent SubmitEvent;
        public UnityEvent PointerMovedToThisEvent;
        public UnityEvent PonterModedFromThisEvent;

        public override Action ClickThis(MenuCommander commander)
        {
            if (SubmitEvent != null) SubmitEvent.Invoke();
            return base.ClickThis(commander);
        }

        public override void Submit(MenuCommander commander)
        {
            if (SubmitEvent != null) SubmitEvent.Invoke();
            base.Submit(commander);
        }

        public override void MovePointerToThis(MenuCommander commander)
        {
            if (PointerMovedToThisEvent != null) PointerMovedToThisEvent.Invoke();
            base.MovePointerToThis(commander);
        }

        public override void MovePointerFromThis()
        {
            if (PonterModedFromThisEvent != null) PonterModedFromThisEvent.Invoke();
            base.MovePointerFromThis();
        }
    }
}
