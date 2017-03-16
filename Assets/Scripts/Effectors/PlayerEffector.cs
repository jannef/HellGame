using fi.tamk.hellgame.utils;
using System.Collections;
using fi.tamk.hellgame.character;
using UnityEngine;

namespace fi.tamk.hellgame.effector
{
    public class PlayerEffector : Effector
    {
        [SerializeField] private float _slowDownScale;
        [SerializeField] private float _slowDownLenght;
        [SerializeField] protected float EffectLength = 1f;
        [SerializeField] private float _startingBlinkingFrequency = 0.1f;
        [SerializeField] private float _endBlinkingFrequency = 0.05f;
        [SerializeField] private AnimationCurve _blinkingEasing;
        [SerializeField] private GameObject _deathParticleEffect;

        private Renderer _renderer;
        private HealthComponent _healthComponent;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
            _healthComponent = GetComponent<HealthComponent>();
        }

        public override void Activate()
        {
            base.Activate();
            Effect.LifeTime = _slowDownLenght * _slowDownScale;
            
            Effect.SetOnstart(SlowDown, new float[2] { _slowDownLenght, _slowDownScale });
            var go = Instantiate(_deathParticleEffect);
            go.transform.position = transform.position;

            if (!gameObject.activeInHierarchy) return;
            if (!(_healthComponent != null && _healthComponent.HasDied))Effect.SetOnstart((args) => StartCoroutine(StaticCoroutines.BlinkCoroutine(_renderer, EffectLength, _startingBlinkingFrequency, _endBlinkingFrequency, _blinkingEasing)), new float[] { });
        }
    }
}
