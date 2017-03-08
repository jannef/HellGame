using fi.tamk.hellgame.character;
using fi.tamk.hellgame.utils;
using System.Collections;
using UnityEngine;

namespace fi.tamk.hellgame.physicsobjects
{
    [RequireComponent(typeof(Rigidbody))]
    class Pickup : MonoBehaviour
    {
        [SerializeField] protected Collider CollisionCollider;
        [SerializeField] protected float Tolerance;
        [SerializeField] protected AnimationCurve Curve;
        [SerializeField] protected float EasingTime;
        [SerializeField] protected float Speed;
        [SerializeField] protected LayerMask PickupLayer;
        [SerializeField] protected PickupType PickupType;
        [SerializeField] protected float LifeTime;
        [SerializeField] private float blinkingLenght = 1f;
        [SerializeField] private float startingBlinkingFrequency = 0.1f;
        [SerializeField] private float endBlinkingFrequency = 0.05f;
        [SerializeField] private AnimationCurve blinkingEasing;

        protected Rigidbody Rigidbody;
        private Renderer _renderer;

        protected void Awake()
        {
            _renderer = GetComponent<Renderer>();
            Rigidbody = GetComponent<Rigidbody>();    
        }
        protected void OnEnable()
        {
            StartCoroutine(LifeTimeRoutine());
        }

        IEnumerator LifeTimeRoutine()
        {
            float timer = 0;
            while (timer < LifeTime)
            {
                timer += Time.deltaTime;
                yield return null;
            }

            
            StartCoroutine(StaticCoroutines.BlinkCoroutine(_renderer, blinkingLenght, startingBlinkingFrequency, endBlinkingFrequency, blinkingEasing));
            timer = 0;

            while (timer < blinkingLenght)
            {
                timer += Time.deltaTime;
                yield return null;
            }
            GameObject go = gameObject;
            Pool.Instance.ReturnObject(ref go);
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (PickupLayer != (PickupLayer | (1 << other.gameObject.layer))) return;
            Rigidbody.isKinematic = true;
            CollisionCollider.enabled = false;

            StartCoroutine(GoToTransform(other.gameObject.transform));
        }

        private IEnumerator GoToTransform(Transform toWhere)
        {
            var time = Time.time;
            while (toWhere != null && (transform.position - toWhere.position).sqrMagnitude > Tolerance)
            {
                var raw = toWhere.position - transform.position;
                var trajectory = (raw).normalized * Time.deltaTime * Speed * Curve.Evaluate((Time.time - time) / EasingTime);

                trajectory = raw.sqrMagnitude < trajectory.sqrMagnitude ? raw : trajectory;
                transform.position += trajectory;
                yield return null;                    
            }

            ServiceLocator.Instance.GetPickupGatherer(toWhere).PickItem(PickupType);
            var go = gameObject;
            Pool.Instance.ReturnObject(ref go);
            yield return null;
        }
    }
}
