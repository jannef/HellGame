using fi.tamk.hellgame.dataholders;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace fi.tamk.hellgame.character
{

    public class UpgradeableBulletEmitter : BulletEmitterWithEffects
    {
        public BulletEmitterUpgradeData UpgradeData;

        private float defaultCooldown;
        private float defaultSpread;
        private int defaultNumberOfBullets;
        private float defaultDispersion;
        private float defaultBulletSpeed;
        private int defaultDamage;

        // Use this for initialization
        void Start()
        {
            defaultCooldown = Cooldown;
            defaultDispersion = Dispersion;
            defaultNumberOfBullets = NumberOfBullets;
            defaultSpread = Spread;
            BulletSystem.GetDamageAndSpeed(out defaultDamage, out defaultBulletSpeed);

            var limitBreak = GetComponentInParent<PlayerLimitBreak>();

            if (limitBreak == null) return;
            limitBreak.limitBreakActivation.AddListener(PowerUpWeapon);
            limitBreak.limitbreakEndEvent.AddListener(RestoreStatsToDefault);
        }

        private void PowerUpWeapon()
        {
            Cooldown += UpgradeData.AddedEmitterCooldown;
            Spread += UpgradeData.AddedSpread;
            NumberOfBullets += UpgradeData.AddedBulletNumber;
            Dispersion += UpgradeData.AddedDispersion;

            BulletSystem.AddToDamageAndSpeed(UpgradeData.AddedDamage, UpgradeData.AddedSpeed);
        }

        private void RestoreStatsToDefault()
        {
            Cooldown = defaultCooldown;
            Dispersion = defaultDispersion;
            Spread = defaultSpread;
            BulletSystem.SetDamageAndSpeed(defaultDamage, defaultBulletSpeed);
            NumberOfBullets = defaultNumberOfBullets;
            Cooldown = defaultCooldown;
        }
    }
}
