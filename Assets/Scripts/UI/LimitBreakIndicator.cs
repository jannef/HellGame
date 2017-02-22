using System;
using System.Collections;
using System.Collections.Generic;
using fi.tamk.hellgame.character;
using UnityEngine;
using UnityEngine.UI;

namespace fi.tamk.hellgame.ui
{

    public class LimitBreakIndicator : MonoBehaviour
    {
        private Text _limitBreakText;

        public void ConnectToPlayer(PlayerLimitBreak playerLimitBreakComponent)
        {
            playerLimitBreakComponent.LimitBreakActivation.AddListener(ActivateLimitBreak);
            playerLimitBreakComponent.PowerUpGained += ChangeLimitBreakText;
            _limitBreakText = GetComponent<Text>();
            int currentNumber;
            int currentMax;

            playerLimitBreakComponent.GetCurrentAmountAndThreshHold(out currentNumber, out currentMax);

            ChangeLimitBreakText(currentNumber, currentMax);
        }

        private void ChangeLimitBreakText(int currentNumber, int maxNumber)
        {
            if (currentNumber < maxNumber)
            {
                _limitBreakText.text = string.Format("{0} / {1}", currentNumber, maxNumber);
                return;
            }

            _limitBreakText.text = LocaleStrings.UI_LimitBreakPrompt;
        }

        private void ActivateLimitBreak()
        {
            _limitBreakText.text = LocaleStrings.UI_LimitBreakNotification;
        }
    }
}
