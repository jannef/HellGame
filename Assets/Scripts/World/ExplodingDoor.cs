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
            StartCoroutine(StaticCoroutines.DoAfterDelay(5f, StartDisappearing));
        }

        private void StartDisappearing()
        {
            StartCoroutine(DoorDisappearingEffect());
        }

        private IEnumerator DoorDisappearingEffect()
        {
            _rigidBody.isKinematic = true;
            var t = 0f;
            var startColor = _renderer.material.color;
            var invisibleColor = new Color(startColor.r, startColor.g, startColor.b, 0);

            while (t <= 1)
            {
                Debug.Log(t);
                t += WorldStateMachine.Instance.DeltaTime;
                _renderer.material.color = Color.Lerp(startColor, invisibleColor, t);
                yield return null;
            }

            _renderer.material.color = invisibleColor;
            yield return null;
        }
    }
}
