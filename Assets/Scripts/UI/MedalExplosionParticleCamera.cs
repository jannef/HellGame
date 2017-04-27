using fi.tamk.hellgame.utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.ui
{

    public class MedalExplosionParticleCamera : ParticleExplosionCamera
    {
        [SerializeField] private GameObject[] _medalParticleSystems;
        protected float Lifetime;

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

            var go = Instantiate(system.gameObject, ExplosionPoint.position, Quaternion.identity);
            go.transform.forward = ExplosionPoint.forward;
            Lifetime = go.GetComponent<ParticleSystem>().main.duration;
            StartCoroutine(StaticCoroutines.DoAfterDelay(Lifetime, Stop));
        }
    }
}
