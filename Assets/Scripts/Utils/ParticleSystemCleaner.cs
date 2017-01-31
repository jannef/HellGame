using UnityEngine;

namespace fi.tamk.hellgame.utils
{
    public class ParticleSystemCleaner : MonoBehaviour
    {
        private ParticleSystem _particleSystem;

        private void Awake()
        {
            _particleSystem = GetComponent<ParticleSystem>();
            if (_particleSystem == null) Destroy(gameObject);
        }

        private void Update()
        {
            if (_particleSystem == null || !_particleSystem.IsAlive()) Destroy(gameObject);
        }
    }
}