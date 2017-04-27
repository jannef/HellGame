using fi.tamk.hellgame.utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.ui
{

    public class MedalExplosionParticleCamera : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private GameObject[] _medalParticleSystems;
        [SerializeField] private Transform _explosionPoint;
        public float lifeTime = 2f;

        public void PlayExplosion(ClearingRank _rank)
        {
            GameObject system = null;

            try
            {
                system = _medalParticleSystems[(int)_rank];
            } catch (Exception e)
            {
                Debug.Log(e);
                return;
            }

            var go = Instantiate(system.gameObject, _explosionPoint.position, Quaternion.identity);
            go.transform.forward = _explosionPoint.forward;
            lifeTime = go.GetComponent<ParticleSystem>().main.duration;
            StartCoroutine(StaticCoroutines.DoAfterDelay(lifeTime, Stop));
        }

        private void Stop()
        {
            _camera.enabled = false;
            Destroy(this.gameObject);
        }
    }
}
