using fi.tamk.hellgame.dataholders;
using System.Collections;
using System.Collections.Generic;
using fi.tamk.hellgame.effector;
using fi.tamk.hellgame.effects;
using UnityEngine;
using UnityEngine.Events;

namespace fi.tamk.hellgame.character
{

    public class BulletEmitterCharging : BulletEmitterWithEffects
    {
        public UnityEvent StartChargingEvent;
        [SerializeField] private float maxChargeTime;
        [SerializeField] private AnimationCurve shotChargeEasing;
        [SerializeField] private float screenShake;
        [SerializeField] private float screenShakeLenght;
        [SerializeField] private BulletEmitterUpgradeData ShotMaxStats;

        private bool isCharging = false;
        private float chargingTimer = 0f;

        private float defaultCooldown;
        private float defaultStartAngle;
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
            defaultStartAngle = StartAngle;
            defaultNumberOfBullets = NumberOfBullets;
            defaultSpread = Spread;
            BulletSystem.GetDamageAndSpeed(out defaultDamage, out defaultBulletSpeed);

            var laserPointer = GetComponent<LaserPointer>();
            laserPointer.Initialize(shotChargeEasing, maxChargeTime);
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

        public override void Fire()
        {
            if (!isCharging)
            {
                isCharging = true;
                StartCharging();
                return;
            }

            ReleaseCharge();
            isCharging = false;
        }

        protected override void Update()
        {
            base.Update();
            if (isCharging && chargingTimer <= maxChargeTime) chargingTimer += Time.deltaTime;
        }

        private void ReleaseCharge()
        {
            var t = shotChargeEasing.Evaluate(chargingTimer / maxChargeTime);

            var speed = Mathf.Lerp(defaultBulletSpeed, defaultBulletSpeed + ShotMaxStats.AddedSpeed, t);
            var damage = Mathf.RoundToInt(Mathf.Lerp(defaultDamage, defaultDamage + ShotMaxStats.AddedDamage, t));

            BulletSystem.SetDamageAndSpeed(damage, speed);

            NumberOfBullets = Mathf.RoundToInt(Mathf.Lerp(defaultNumberOfBullets, defaultNumberOfBullets + ShotMaxStats.AddedBulletNumber, t));
            Spread = Mathf.Lerp(defaultSpread, defaultSpread + ShotMaxStats.AddedSpread, t);
            Dispersion = Mathf.Lerp(Dispersion, Dispersion + ShotMaxStats.AddedDispersion, t);

            base.Fire();

            Effector.ScreenShakeEffect(new float[2] { screenShake * t, screenShakeLenght});
            Effector.ThreadFreezeFrame(new float[0]);

            chargingTimer = 0f;

            RestoreStatsToDefault();
        }

        private void StartCharging()
        {
            StartChargingEvent.Invoke();
        }
    }
}
