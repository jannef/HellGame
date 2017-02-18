using fi.tamk.hellgame.dataholders;

namespace fi.tamk.hellgame.character
{

    public class UpgradeableBulletEmitter : BulletEmitter
    {
        public BulletEmitterUpgradeData UpgradeData;

        protected override void Awake()
        {
            base.Awake();
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

            BulletSystem.Damage += (int)UpgradeData.AddedDamage;
            BulletSystem.Speed += UpgradeData.AddedSpeed;
        }
    }
}
