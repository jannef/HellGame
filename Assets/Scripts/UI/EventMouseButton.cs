using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace fi.tamk.hellgame.ui
{

    public class EventMouseButton : InteractableUiElementAbstract
    {
        public UnityEvent ClickEvent;
        [SerializeField] private float maxHoldDownLenght = 2.5f;

        public override Action ClickThis(MenuCommander commander)
        {
            if (ClickEvent != null) ClickEvent.Invoke();
            StartCoroutine(ButtonHeldDownRoutine());
            return MouseUpAction;
        }

        public void MouseUpAction()
        {
            StopAllCoroutines();
        }

        public override void MovePointerFromThis()
        {
        }

        public override void MovePointerToHisWithoutSound(MenuCommander commander)
        {
        }

        public override void MovePointerToThis(MenuCommander commander)
        {
        }

        private IEnumerator ButtonHeldDownRoutine()
        {
            var t = 0f;
            yield return null;

            while (t <= maxHoldDownLenght)
            {
                t += Time.unscaledDeltaTime;
                if (ClickEvent != null)
                {
                    ClickEvent.Invoke();
                }
                yield return null;
            }

            MouseUpAction();
        }
    }
}
