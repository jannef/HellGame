using fi.tamk.hellgame.dataholders;
using fi.tamk.hellgame.effector;
using fi.tamk.hellgame.effects;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace fi.tamk.hellgame.character
{

    public class BulletEmitterCharging : BulletEmitter
    {
        public UnityEvent StartChargingEvent;

        [SerializeField] private float maxChargeTime;
        [SerializeField] private AnimationCurve shotChargeEasing;
        [SerializeField] private float screenShake;
        [SerializeField] private float screenShakeLenght;
        [SerializeField] private BulletEmitterUpgradeData ShotMaxStats;

        private bool _isCharging = false;
        private float _chargingTimer = 0f;

        protected override void Awake()
        {
            base.Awake();

            var laserPointer = GetComponent<LaserPointer>();
            laserPointer.Initialize(shotChargeEasing, maxChargeTime);
        }

        public override void Fire()
        {
            if (!_isCharging)
            {
                _isCharging = true;
                StartCharging();
            }
            else
            {
                _isCharging = false;
                ReleaseCharge();
            }
        }

        protected override void Update()
        {
            base.Update();
            if (_isCharging && _chargingTimer <= maxChargeTime) _chargingTimer += Time.deltaTime;
        }

        private void ReleaseCharge()
        {
            var t = shotChargeEasing.Evaluate(_chargingTimer / maxChargeTime);

            if (t <= 0.001)
            {
                _chargingTimer = 0f;
                RestoreStatsToDefault();
                FiringEvent.Invoke();
                return;
            }

            var speed = Mathf.Lerp(DefaultBulletSpeed, DefaultBulletSpeed + ShotMaxStats.AddedSpeed, t);
            var damage = Mathf.RoundToInt(Mathf.Lerp(DefaultDamage, DefaultDamage + ShotMaxStats.AddedDamage, t));

            BulletSystem.Damage = damage;
            BulletSystem.Speed = speed;

            NumberOfBullets = Mathf.RoundToInt(Mathf.Lerp(DefaultNumberOfBullets, DefaultNumberOfBullets + ShotMaxStats.AddedBulletNumber, t));
            Spread = Mathf.Lerp(DefaultSpread, DefaultSpread + ShotMaxStats.AddedSpread, t);
            Dispersion = Mathf.Lerp(Dispersion, Dispersion + ShotMaxStats.AddedDispersion, t);

            StartCoroutine(FiringCoroutine(3, t));
        }

        private void StartCharging()
        {
            StartChargingEvent.Invoke();
        }

        private IEnumerator FiringCoroutine(int shotAmount, float t)
        {
            Effector.ScreenShakeEffect(new float[2] { screenShake * t, screenShakeLenght * shotAmount });
            Effector.ThreadFreezeFrame(new float[0]);

            for (int i = 0; i < shotAmount; i++)
            {
                base.Fire();
                yield return null;
            }

            

            _chargingTimer = 0f;

            RestoreStatsToDefault();
        }
    }
}
