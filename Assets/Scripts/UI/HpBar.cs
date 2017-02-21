
using System;
using fi.tamk.hellgame.character;
using UnityEngine;

namespace fi.tamk.hellgame.ui
{
    public class HpBar : ProgressBar
    {
        [SerializeField] protected HealthComponent _whichToTrackInitially; // will probably be removed!
        private HealthComponent _trackedHealth;

        protected override void Awake()
        {
            base.Awake();
            AttachToHealthComponent(_whichToTrackInitially);
        }

        private void TrackHealthChange(float percentage, int currentHp, int maxHp)
        {
            if (currentHp <= 0) _trackedHealth.HealthChangeEvent -= TrackHealthChange;
            SetBarProgress(percentage);
        }

        public void AttachToHealthComponent(HealthComponent toWhich)
        {
            if (_trackedHealth != null) _trackedHealth.HealthChangeEvent -= TrackHealthChange;
            toWhich.HealthChangeEvent += TrackHealthChange;
            _trackedHealth = toWhich;
        }
    }
}
