using fi.tamk.hellgame.utils;
using System.Collections;
using fi.tamk.hellgame.character;
using UnityEngine;
using System;
using fi.tamk.hellgame.world;

namespace fi.tamk.hellgame.effector
{
    public class PlayerEffector : Effector
    {
        [SerializeField] private float _slowDownScale;
        [SerializeField] private float _slowDownLenght;
        [SerializeField] protected float EffectLength = 1f;
        [SerializeField] private AnimationCurve _slowDownCurve;
        [SerializeField] private GameObject _deathParticleEffect;
        [FMODUnity.EventRef]
        public String DeathSound = "";

        public override void Activate()
        {
            base.Activate();
            Effect.LifeTime = _slowDownLenght * _slowDownScale;
            Utilities.PlayOneShotSound(DeathSound, transform.position);
            WorldStateMachine.Instance.CurvedSlowDown(_slowDownLenght, _slowDownScale, 1, _slowDownCurve);
            var go = Instantiate(_deathParticleEffect);
            go.transform.position = transform.position;
        }
    }
}
