using fi.tamk.hellgame.utils.Stairs.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledParticle : MonoBehaviour {
    private ParticleSystem _particleSystem;
    private float _lifeTimeTimer;

	void OnEnable()
    {
        _lifeTimeTimer = _particleSystem.main.duration;

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
