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
        [SerializeField] private AnimationCurve _blinkingEasing;
        [SerializeField] private GameObject _deathParticleEffect;

        public override void Activate()
        {
            base.Activate();
            Effect.LifeTime = _slowDownLenght * _slowDownScale;
            
            Effect.SetOnstart(SlowDown, new [] { _slowDownLenght, _slowDownScale });
            var go = Instantiate(_deathParticleEffect);
            go.transform.position = transform.position;
        }
    }
}
