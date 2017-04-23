using fi.tamk.hellgame.utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.ui
{

    public class MedalExplosionParticleCamera : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private ParticleSystem _particleSystem;
        float lifeTime = 2f;

        // Use this for initialization
        void Start()
        {
            lifeTime = _particleSystem.main.duration;
            StartCoroutine(StaticCoroutines.DoAfterDelay(lifeTime, Stop));
        }

        private void Stop()
        {
            _camera.enabled = false;
            Destroy(this.gameObject);
        }
    }
}
