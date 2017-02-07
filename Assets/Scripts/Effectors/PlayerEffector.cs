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
        [SerializeField] private float _effectLength = 1f;
        [SerializeField] private float _startingBlinkingFrequency = 0.1f;
        [SerializeField] private float _endBlinkingFrequency = 0.05f;
        [SerializeField] private AnimationCurve _blinkingEasing;

        private Renderer _renderer;

        public override void Activate()
        {
            base.Activate();
            Effect.LifeTime = _effectLength;
            _renderer = GetComponent<Renderer>();
            Effect.SetOnstart(ScreenShakeEffect, new float[2] { _shakeIntensity, _shakeLenght });
            if (!gameObject.activeInHierarchy) return;
            StartCoroutine(StaticCoroutines.BlinkCoroutine(_renderer, _effectLength, _startingBlinkingFrequency, _endBlinkingFrequency, _blinkingEasing));
        }
    }
}
