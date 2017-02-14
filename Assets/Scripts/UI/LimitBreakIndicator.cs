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
        [SerializeField] private PlayerLimitBreak _testPlayerCoupling;


        private void Start()
        {
            _limitBreakText = GetComponent<Text>();
            _testPlayerCoupling.limitBreakActivation.AddListener(ActivateLimitBreak);
            _testPlayerCoupling.powerUpGained += ChangeLimitBreakText;
        }

        private void ChangeLimitBreakText(int currentNumber, int maxNumber)
        {
            if (currentNumber < maxNumber)
            {
                _limitBreakText.text = string.Format("{0} / {1}", currentNumber, maxNumber);
                return;
            }

            _limitBreakText.text = "PRESS TO ACTIVATE LIMIT BREAK!";
        }

        private void ActivateLimitBreak()
        {
            _limitBreakText.text = "ULTIMATE POWER!";
        }
    }
}
