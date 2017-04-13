using fi.tamk.hellgame.world;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.effect
{

    public class SizeChangeEffect : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _sizeLerp;
        [SerializeField] private float _changeDuration;
        [SerializeField] private Vector3 _finalSize;
        [SerializeField] private Transform _remoteChild;
        public bool DestroyAfterEffect = false;

        public void StartChangingSize()
        {
            //if (_remoteChild == null) throw new UnityException("SizeChangeEffect _remoteChild variable not set. Track Blob boss transform into the serialized field");
            StartCoroutine(SizeChangeCoroutine());
        }

        private IEnumerator SizeChangeCoroutine()
        {
            var t = 0f;
            var originalScale = transform.localScale;

            while (t <= _changeDuration)
            {
                t += WorldStateMachine.Instance.DeltaTime;
                var ratio = t / _changeDuration; 
                transform.localScale = Vector3.Lerp(originalScale, _finalSize, _sizeLerp.Evaluate(ratio));
                yield return null;
            }

            transform.localScale = _finalSize;
            if (DestroyAfterEffect)
            {
                Destroy(gameObject);
            }
            yield return null;
        }
    }
}
