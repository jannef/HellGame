using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.debug
{
    public class ParticleDestroyer : MonoBehaviour
    {
        private ParticleSystem _particleSystem;

        void OnEnable()
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }

        void LateUpdate()
        {
            if (!_particleSystem.IsAlive()) Destroy(gameObject);
        }
    }
}
