using System.Collections;
using System.Collections.Generic;
using fi.tamk.hellgame.effector;
using fi.tamk.hellgame.effects;
using fi.tamk.hellgame.utils;
using UnityEngine;

namespace fi.tamk.hellgame.effectors
{
    public class LimitBreakEffector : PlayerEffector
    {
        [SerializeField] private PlayerLimitBreakStats _limitBreakStats;
        [SerializeField] private GameObject _particleSystem;
        [SerializeField] private GameObject _ambientParticleSystem;

        private void Start()
        {
            _effectLength = _limitBreakStats.LimitBreakLenght;
        }

        public override void Activate()
        {
            base.Activate();

            if (_particleSystem == null) return;
            GameObject go = Instantiate(_particleSystem);
            ServiceLocator.Instance.MainCameraScript.PlaceParticleEffectInfrontOfCamera(go.transform, 6);
            GameObject ambientGO = Instantiate(_ambientParticleSystem);
            ServiceLocator.Instance.MainCameraScript.PlaceParticleEffectInfrontOfCamera(ambientGO.transform, 6);
        }
    }
}
