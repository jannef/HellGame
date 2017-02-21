using fi.tamk.hellgame.utils;
using System.Collections;
using UnityEngine;

namespace fi.tamk.hellgame.effector
{
    public class PlayerEffector : Effector
    {
        [SerializeField] private float _shakeIntensity;
        [SerializeField] private float _shakeLenght;
        [SerializeField] private float _slowDownScale;
        [SerializeField] private float _slowDownLenght;
        [SerializeField] protected float _effectLength = 1f;
        [SerializeField] private float _startingBlinkingFrequency = 0.1f;
        [SerializeField] private float _endBlinkingFrequency = 0.05f;
        [SerializeField] private AnimationCurve _blinkingEasing;
        [SerializeField] private GameObject _deathParticleEffect;

        private Renderer _renderer;

        public override void Activate()
        {
            base.Activate();
            Effect.LifeTime = _slowDownLenght * _slowDownScale;
            _renderer = GetComponent<Renderer>();
            Effect.SetOnstart(SlowDown, new float[2] { _slowDownLenght, _slowDownScale });
            Effect.SetOnEnd(ScreenShakeEffect, new float[2] { _shakeIntensity, _shakeLenght });
            GameObject go = Instantiate(_deathParticleEffect);
            go.transform.position = transform.position;

            if (!gameObject.activeInHierarchy) return;
            StartCoroutine(StaticCoroutines.BlinkCoroutine(_renderer, _effectLength, _startingBlinkingFrequency, _endBlinkingFrequency, _blinkingEasing));
        }
    }
}
