using System.Collections;
using System.Collections.Generic;
using fi.tamk.hellgame.utils;
using UnityEngine;

namespace fi.tamk.hellgame.ui
{
    public class ParticleExplosionCamera : MonoBehaviour
    {
        [SerializeField] protected Camera Camera;
        [SerializeField] protected Transform ExplosionPoint;
        [SerializeField] protected GameObject Prefab;
        protected ParticleSystem Particles;

        public void PlayUsingDraggedInPrefab()
        {
            PlayParticles(Prefab);
        }

        public void PlayParticles(GameObject prefab)
        {
            var go = Instantiate(prefab, ExplosionPoint.position, Quaternion.identity);
            go.transform.forward = ExplosionPoint.forward;
            
            StartCoroutine(WaitForTheSystemToDie(go.GetComponent<ParticleSystem>()));
        }

        protected void Stop()
        {
            Camera.enabled = false;
            Destroy(gameObject);
        }

        protected IEnumerator WaitForTheSystemToDie(ParticleSystem system)
        {
            while (system.IsAlive())
            {
                yield return null;
            }

            Stop();
        }
    }
}