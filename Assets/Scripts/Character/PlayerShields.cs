using fi.tamk.hellgame.character;
using fi.tamk.hellgame.utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.character
{

    public class PlayerShields : MonoBehaviour
    {
        private ParticleSystem _shieldParticle;
        private float effectLenght;
        private Vector3 _minScale;
        private Vector3 _originalScale;
        [FMODUnity.EventRef]
        public String ShieldSoundEvent = "";
        private FMOD.Studio.EventInstance _shieldAmbientSoundEvent;

        // Use this for initialization
        void Awake()
        {
            var hc = GetComponentInParent<HealthComponent>();
            _shieldParticle = GetComponent<ParticleSystem>();
            if (hc == null) return;
            effectLenght = hc.InvulnerabilityLenght - 0.21f;
            hc.HitFlinchEffect.AddListener(ActivateShields);
        }

        void ActivateShields()
        {
            _shieldParticle.Play();
            StartCoroutine(StaticCoroutines.DoAfterDelay(effectLenght, DeactivateShield));
            _shieldAmbientSoundEvent = FMODUnity.RuntimeManager.CreateInstance(ShieldSoundEvent);
            _shieldAmbientSoundEvent.start();
        }

        void DeactivateShield()
        {
            _shieldParticle.Stop();
            if (_shieldAmbientSoundEvent == null) return;
            _shieldAmbientSoundEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            _shieldAmbientSoundEvent.release();
            _shieldAmbientSoundEvent = null;
        }

        void OnDestroy()
        {
            if (_shieldAmbientSoundEvent == null) return;
            _shieldAmbientSoundEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            _shieldAmbientSoundEvent.release();
            _shieldAmbientSoundEvent = null;
        }
    }
}
