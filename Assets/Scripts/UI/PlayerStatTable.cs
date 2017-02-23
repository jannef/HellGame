using System.Collections;
using System.Collections.Generic;
using fi.tamk.hellgame.character;
using UnityEngine;
using UnityEngine.UI;

namespace fi.tamk.hellgame.ui
{

    public class PlayerStatTable : MonoBehaviour
    {
        public void SetUpPlayerStatTable(GameObject playerGO)
        {
            LocaleStrings.CurrentLocale = LocaleStrings.en_EN;

            var _playerHealthIndicator = GetComponentInChildren<PlayerHealthIndicator>();

            var limitBreakComponent = playerGO.GetComponent<PlayerLimitBreak>();

            var healthComponent = playerGO.GetComponent<HealthComponent>();

            if (healthComponent != null) _playerHealthIndicator.ConnectToPlayer(healthComponent);
        }
    }
}
