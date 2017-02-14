using System.Collections;
using System.Collections.Generic;
using fi.tamk.hellgame.effector;
using fi.tamk.hellgame.effects;
using UnityEngine;

namespace fi.tamk.hellgame.effectors
{
    public class LimitBreakEffector : PlayerEffector
    {
        [SerializeField] private PlayerLimitBreakStats _limitBreakStats;
        [SerializeField] private GameObject _particleSystem;

        private void Start()
        {
            _effectLength = _limitBreakStats.LimitBreakLenght;
        }

        public override void Activate()
        {
            base.Activate();
            if (_particleSystem == null) return;
            GameObject go = Instantiate(_particleSystem);
            ScreenParticleEffectPlacer.Instance.PlaceParticleEffectInfrontOfCamera(go.transform, 6);
        }
    }
}
