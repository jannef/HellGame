using fi.tamk.hellgame.utils;
using UnityEngine;

namespace fi.tamk.hellgame.WORK_IN_PROGRESS
{
    public class PooledParticle : MonoBehaviour
    {
        private ParticleSystem _particleSystem;
        private float _lifeTimeTimer;

        void OnEnable()
        {
            _particleSystem = GetComponent<ParticleSystem>();
            _lifeTimeTimer = _particleSystem.main.duration;
            _particleSystem.Play();
        }

        void LateUpdate()
        {
            _lifeTimeTimer -= Time.deltaTime;

            if (_lifeTimeTimer <= 0)
            {
                GameObject go = this.gameObject;
                Pool.Instance.ReturnObject(ref go);
            }
        }
    }
}
