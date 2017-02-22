
using System;
using fi.tamk.hellgame.character;
using UnityEngine;

namespace fi.tamk.hellgame.ui
{
    public class HpBar : ProgressBar
    {
        [SerializeField] protected HealthComponent _whichToTrackInitially; // will probably be removed!
        [SerializeField] protected float DamageDecayRate = 0.011f;
        private HealthComponent _trackedHealth;

        private float _latentDamage = 0f;
        private float _latestHitpoints = 1f;

        protected override void Awake()
        {
            base.Awake();
            AttachToHealthComponent(_whichToTrackInitially);
        }

        private void TrackHealthChange(float percentage, int currentHp, int maxHp)
        {
            if (currentHp <= 0) _trackedHealth.HealthChangeEvent -= TrackHealthChange;

            _latentDamage += _latestHitpoints - percentage;
            _latestHitpoints = percentage;
            SetBarProgress(percentage, _latentDamage);
        }

        public void AttachToHealthComponent(HealthComponent toWhich)
        {
            if (_trackedHealth != null) _trackedHealth.HealthChangeEvent -= TrackHealthChange;
            toWhich.HealthChangeEvent += TrackHealthChange;
            _trackedHealth = toWhich;
        }

        private void Update()
        {
            _latentDamage = Mathf.Clamp(_latentDamage - (DamageDecayRate * Time.deltaTime), 0f, 1f);
            SetBarProgress(_latestHitpoints, _latentDamage);
        }
    }
}
