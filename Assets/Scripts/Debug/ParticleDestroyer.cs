using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.debug
{
    public class ParticleDestroyer : MonoBehaviour
    {
        private ParticleSystem _particleSystem;
        private float _lifeTimeTimer;

        void OnEnable()
        {
            _particleSystem = GetComponent<ParticleSystem>();
            _lifeTimeTimer = _particleSystem.main.duration;
        }

        void LateUpdate()
        {
            _lifeTimeTimer -= Time.deltaTime;

            if (_lifeTimeTimer <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
