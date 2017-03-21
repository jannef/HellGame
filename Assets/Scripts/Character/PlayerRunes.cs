using fi.tamk.hellgame.character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.character
{

    public class PlayerRunes : MonoBehaviour
    {
        private ParticleSystem[] _runes;
        private IdleRotation _idleRotation;
        [SerializeField] private float rotationSpeedIncreasePerHit;
        [SerializeField] private GameObject runeExplosionParticle;
        private float startRotationSpeed;
        [SerializeField] private int maxActiveRuneAmount = 4;
        private int _amountOfActiveRunes;

        private void Awake()
        {
            _amountOfActiveRunes = maxActiveRuneAmount;
            var hc = GetComponentInParent<HealthComponent>();
            if (hc == null) Debug.Log("null");
            if (hc != null) hc.HealthChangeEvent += UpdateRunes;
            _runes = GetComponentsInChildren<ParticleSystem>();
            _idleRotation = GetComponentInChildren<IdleRotation>();
            startRotationSpeed = _idleRotation._rotationSpeed;
        }

        public void UpdateRunes(float percentage, int currentHp, int maxHp)
        {
            Debug.Log(currentHp);
            if (currentHp == _amountOfActiveRunes) return;

            if (currentHp < _amountOfActiveRunes)
            {
                int amountToDeActivate = _amountOfActiveRunes - currentHp;

                foreach (ParticleSystem system in _runes)
                {
                    if (system.isPlaying)
                    {
                        system.Stop();
                        amountToDeActivate--;

                        GameObject instantiatedExplosion = Instantiate(runeExplosionParticle, system.transform);
                        instantiatedExplosion.transform.localPosition = Vector3.zero;


                        if (amountToDeActivate <= 0)
                        {
                            break;
                        }
                    }
                }

            } else
            {
                int amountActivate = currentHp - _amountOfActiveRunes;

                foreach (ParticleSystem system in _runes)
                {
                    if (!system.isPlaying)
                    {
                        system.Play();
                        amountActivate--;
                        if (amountActivate <= 0)
                        {
                            break;
                        }
                    }
                }
            }

            _amountOfActiveRunes = currentHp;
            UpdateRotation();
        }

        private void UpdateRotation()
        {
            _idleRotation._rotationSpeed = startRotationSpeed + ((maxActiveRuneAmount - _amountOfActiveRunes) * rotationSpeedIncreasePerHit);
        }
    }
}
