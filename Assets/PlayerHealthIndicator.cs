using System;
using System.Collections;
using System.Collections.Generic;
using fi.tamk.hellgame.character;
using UnityEngine;
using UnityEngine.UI;

namespace fi.tamk.hellgame.ui
{

    public class PlayerHealthIndicator : MonoBehaviour
    {
        private Text text;

        void HealthChanged(float percentage, int currentHp, int maxHp)
        {
            if (text == null)
            {
                text = GetComponent<Text>();
            }

            text.text = currentHp.ToString();
        }

        public void ConnectToPlayer(HealthComponent hc)
        {
            hc.HealthChangeEvent += HealthChanged;
            HealthChanged(0, hc.Health, 0);
        }
    }
}
