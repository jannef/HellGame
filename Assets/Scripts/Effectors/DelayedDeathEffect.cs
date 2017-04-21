using fi.tamk.hellgame.character;
using fi.tamk.hellgame.effector;
using fi.tamk.hellgame.utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.effectors
{

    public class DelayedDeathEffect : Effector
    {
        [SerializeField] private float _lifeTime;
        [SerializeField] private int explosionAmount;
        [SerializeField] private AnimationCurve _explosionSpeadCurve;
        [SerializeField] private GameObject _explosionPrefab;
        [SerializeField] private GameObject _finalExplosion;
        [SerializeField] private Transform _explosionPointsParent;
        private Transform[] _explosionPoints;
        [FMODUnity.EventRef]
        public String smallExplosionSound = "";
        [FMODUnity.EventRef]
        public String finalExplosionSound = "";

        public override void Activate()
        {
            var actorComponent = GetComponent<ActorComponent>();
            actorComponent.enabled = false;
            _explosionPoints = _explosionPointsParent.GetComponentsInChildren<Transform>();
            StartCoroutine(ExplosionRoutine());
        }

        private IEnumerator ExplosionRoutine()
        {
            var t = 0f;
            var explosionIntervalTimer = 0f;
            float explosionInterval = 1f / explosionAmount;

            while (t < 1f)
            {
                t += Time.deltaTime / _lifeTime;

                if (explosionIntervalTimer <= _explosionSpeadCurve.Evaluate(t))
                {
                    explosionIntervalTimer += explosionInterval;
                    var go = Instantiate(_explosionPrefab);
                    int explosionIndex = UnityEngine.Random.Range(0, _explosionPoints.Length);
                    Utilities.PlayOneShotSound(smallExplosionSound, transform.position);
                    go.transform.position = _explosionPoints[Mathf.Clamp(explosionIndex, 0, _explosionPoints.Length)].position;
                    go.transform.forward = _explosionPoints[Mathf.Clamp(explosionIndex, 0, _explosionPoints.Length)].forward;
                }

                yield return null;
            }

            Utilities.PlayOneShotSound(finalExplosionSound, transform.position);
            var explosion = Instantiate(_finalExplosion);
            explosion.transform.position = transform.position;

            ReturnToPool();
            yield return null;
        }

        void ReturnToPool()
        {
            var go = gameObject;
            Pool.Instance.ReturnObject(ref go, true);
        }
    }
}
