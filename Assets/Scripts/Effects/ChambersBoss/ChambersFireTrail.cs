using UnityEngine;
using System.Collections;
using fi.tamk.hellgame.world;
using fi.tamk.hellgame.utils;

namespace fi.tamk.hellgame.effects
{
    public class ChambersFireTrail : MonoBehaviour
    {
        [SerializeField] protected float Duration;
        [SerializeField] protected AnimationCurve Curve;
        [SerializeField] protected bool ReturnToPool;

        private ParticleSystem _indicator;
        private GameObject _ref;
        public void Initialize()
        {
            if (_indicator == null) _indicator = gameObject.GetComponent<ParticleSystem>();
            _ref = gameObject;
            _indicator.Stop();
        }

        public void StartRun(Vector3 to)
        {
            Initialize();
            StartCoroutine(RunTrail(to, Duration));
        }

        private IEnumerator RunTrail(Vector3 target, float duration)
        {
            if (duration < 0.1f) duration = 0.1f;
            _indicator.Play();

            var start = transform.position;
            var timer = 0f;
            while (timer < duration)
            {
                timer += WorldStateMachine.Instance.DeltaTime;
                var ratio = timer / duration;
                transform.position = Vector3.Lerp(start, target, Curve.Evaluate(ratio));
                yield return null;
            }

            transform.position = target;

            _indicator.Stop();
            timer = 0f;
            while (timer < _indicator.main.startLifetimeMultiplier)
            {
                timer += WorldStateMachine.Instance.DeltaTime;
                yield return null;
            }            
            Pool.Instance.ReturnObject(ref _ref, !ReturnToPool);
        }
    }
}
