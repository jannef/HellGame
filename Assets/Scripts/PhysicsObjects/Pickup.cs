using fi.tamk.hellgame.character;
using fi.tamk.hellgame.utils;
using System.Collections;
using fi.tamk.hellgame.world;
using UnityEngine;

namespace fi.tamk.hellgame.physicsobjects
{
    [RequireComponent(typeof(Rigidbody))]
    class Pickup : MonoBehaviour
    {
        [SerializeField] protected float Tolerance;
        [SerializeField] protected AnimationCurve Curve;
        [SerializeField] protected float EasingTime;
        [SerializeField] protected float Speed;
        [SerializeField] protected PickupType PickupType;
        [SerializeField] protected float LifeTime;
        [SerializeField] private float blinkingLenght = 1f;
        [SerializeField] private float startingBlinkingFrequency = 0.1f;
        [SerializeField] private float endBlinkingFrequency = 0.05f;
        [SerializeField] private AnimationCurve blinkingEasing;
        private bool isGathereable = true;

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

        protected void OnDisable()
        {
            Rigidbody.velocity = Vector3.zero;
            StopAllCoroutines();
        }

        public void DisablePickupTemporarily(float forHowLong)
        {
            StartCoroutine(DisableRoutine(forHowLong));
        }

        protected IEnumerator DisableRoutine(float lenght)
        {
            float timer = 0f;
            isGathereable = false;
            while (timer < lenght)
            {
                timer += WorldStateMachine.Instance.DeltaTime;
                yield return null;
            }
            isGathereable = true;
        }

        protected IEnumerator LifeTimeRoutine()
        {
            float timer = 0;
            while (timer < LifeTime)
            {
                timer += WorldStateMachine.Instance.DeltaTime;
                yield return null;
            }

            
            StartCoroutine(StaticCoroutines.BlinkCoroutine(_renderer, blinkingLenght, startingBlinkingFrequency, endBlinkingFrequency, blinkingEasing));
            timer = 0;

            while (timer < blinkingLenght)
            {
                timer += WorldStateMachine.Instance.DeltaTime;
                yield return null;
            }
            GameObject go = gameObject;
            Pool.Instance.ReturnObject(ref go);
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (isGathereable) StartCoroutine(GoToTransform(other.gameObject.transform));
        }

        private IEnumerator GoToTransform(Transform toWhere)
        {
            var time = Time.time;
            while (toWhere != null && (transform.position - toWhere.position).magnitude > Tolerance)
            {
                var raw = toWhere.position - transform.position;
                var trajectory = (raw).normalized * WorldStateMachine.Instance.DeltaTime * Speed * Curve.Evaluate((Time.time - time) / EasingTime);

                trajectory = raw.magnitude < trajectory.magnitude ? raw : trajectory;
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
