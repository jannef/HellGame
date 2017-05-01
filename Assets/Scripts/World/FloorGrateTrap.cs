using fi.tamk.hellgame.character;
using fi.tamk.hellgame.utils;
using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace fi.tamk.hellgame.world
{

    public class FloorGrateTrap : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _telegraphParticles;
        [SerializeField] private ParticleSystem _mainFlameParticles;
        [SerializeField] private Collider _damagingCollider;
        [SerializeField] private float _telegraphLength;
        [SerializeField] private float _flameActiveLenght;
        [SerializeField] private float _coolDownLenght;
        [SerializeField] private float _startCoolDown;

        [SerializeField] private bool _leavePermanentlyOn = false;

        [EventRef]
        public String TelegraphcSoundEvent = "";
        [EventRef]
        public String StartSoundEvent = "";

        public UnityEvent StartFlame;

        private bool running = false;

        private void Awake()
        {
            _damagingCollider.enabled = false;
        }

        public void Activate()
        {
            running = true;
            StartCoroutine(StaticCoroutines.DoAfterDelay(_coolDownLenght - _startCoolDown, StartTelegraph));
        }

        private void StartTelegraph()
        {
            if (!running)
            {
                Stop();
                return;
            }

            _telegraphParticles.Play();
            Utilities.PlayOneShotSound(TelegraphcSoundEvent, transform.position);

            StartCoroutine(StaticCoroutines.DoAfterDelay(_telegraphLength, StartFlames));
        }

        private void StartFlames()
        {
            if (!running)
            {
                Stop();
                return;
            }

            if (StartFlame != null) StartFlame.Invoke();
            _telegraphParticles.Stop();
            _mainFlameParticles.Play();
            _damagingCollider.enabled = true;
            Utilities.PlayOneShotSound(StartSoundEvent, transform.position);

            if (!_leavePermanentlyOn) StartCoroutine(StaticCoroutines.DoAfterDelay(_flameActiveLenght, StopFlames));
        }

        private void StopFlames()
        {
            _damagingCollider.enabled = false;
            _mainFlameParticles.Stop();
            StartCoroutine(StaticCoroutines.DoAfterDelay(_coolDownLenght, StartTelegraph));
        }

        public void Stop()
        {
            _damagingCollider.enabled = false;
            _mainFlameParticles.Stop();
            _telegraphParticles.Stop();
            running = false;
            StopAllCoroutines();
        }
    }
}
