using fi.tamk.hellgame.character;
using fi.tamk.hellgame.utils;
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
        }

        void DeactivateShield()
        {
            _shieldParticle.Stop();
        }
    }
}
