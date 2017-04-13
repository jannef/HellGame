using fi.tamk.hellgame.utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.world
{

    public class ExplodingDoor : MonoBehaviour
    {
        private Rigidbody _rigidBody;
        [SerializeField]
        private float _explosionForce;
        [SerializeField]
        private float _explosionRadius;
        [SerializeField]
        private Transform _explosionPoint;
        [SerializeField]
        private float _fadeDuration = 10f;
        private Renderer _renderer;

        // Use this for initialization
        void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _renderer = GetComponent<Renderer>();
        }

        public void Explode()
        {
            _rigidBody.isKinematic = false;
            _rigidBody.AddExplosionForce(_explosionForce, _explosionPoint.position, _explosionRadius);
            StartCoroutine(StaticCoroutines.DoAfterDelay(_fadeDuration, StartDisappearing));
        }

        private void StartDisappearing()
        {
            StartCoroutine(DoorDisappearingEffect());
        }

        private IEnumerator DoorDisappearingEffect()
        {
            var t = 0f;
            var startColor = _renderer.material.color;
            var invisibleColor = new Color(startColor.r, startColor.g, startColor.b, 0);

            while (t <= 1)
            {
                t += WorldStateMachine.Instance.DeltaTime;
                _renderer.material.color = Color.Lerp(startColor, invisibleColor, t);
                yield return null;
            }

            _renderer.material.color = invisibleColor;
            Destroy(gameObject);
            yield return null;
        }
    }
}
