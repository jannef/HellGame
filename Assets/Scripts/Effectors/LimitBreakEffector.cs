﻿using System.Collections;
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
            EffectLength = _limitBreakStats.DesiredBaseLenght;
            
        }

        public override void Activate()
        {
            base.Activate();

            if (_particleSystem == null) return;
            GameObject go = Instantiate(_particleSystem);
            go.transform.position = Effect.transform.position;
            go.transform.forward = Effect.transform.forward;
            go.transform.SetParent(Effect.transform);

            GameObject ambientGO = Instantiate(_ambientParticleSystem);
            ambientGO.transform.forward = Effect.transform.forward;
            ambientGO.transform.position = Effect.transform.position;
            ambientGO.transform.SetParent(Effect.transform);

            Effect.SetOnstart((args) => StartCoroutine(ParticleEffectDisable(_limitBreakStats.DesiredBaseLenght, ambientGO.GetComponent<ParticleSystem>())), new float[] { });

            ServiceLocator.Instance.MainCameraScript.PlaceParticleEffectInfrontOfCamera(Effect.transform, 6);

            Effect.LifeTime = _limitBreakStats.DesiredBaseLenght + 1f;
        }

        private IEnumerator ParticleEffectDisable(float duration, ParticleSystem particleSystem)
        {
            var t = 0f;

            while (t <= 1)
            {
                t += Time.deltaTime / duration;

                yield return null;
            }

            particleSystem.Stop();
        }
    }
}
