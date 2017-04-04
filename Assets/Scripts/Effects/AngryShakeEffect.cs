using fi.tamk.hellgame.world;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

namespace fi.tamk.hellgame.effects
{
    public class AngryShakeEffect : MonoBehaviour
    {
        [SerializeField] private Transform _transform;
        [Range(0.01f, 10f), SerializeField] private float _amounth = 0.2f;

        public void Awake()
        {
            if (_transform == null) throw new UnityException("AngryShakeEffect MonoBehaviour component with no configured transfom to shake in GO: " + gameObject);
        }

        public void Activate(float duration)
        {
            StartCoroutine(Shake(duration));
        }

        private IEnumerator Shake(float duration)
        {
            Vector3 original = _transform.position;
            var timer = 0f;

            while (timer < duration)
            {
                if (_transform == null) yield break;

                var offset = Random.insideUnitSphere * _amounth;
                offset.y = 0;
                _transform.position = original + offset;

                timer += WorldStateMachine.Instance.DeltaTime;
                yield return null;
            }

            _transform.position = original;
        }
    }
}
