﻿using fi.tamk.hellgame.utils.Stairs.Utils;
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
        protected Rigidbody Rigidbody;

        protected void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
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
            while (toWhere != null && (transform.position - toWhere.position).magnitude > Tolerance)
            {
                var raw = toWhere.position - transform.position;
                var trajectory = (raw).normalized * Time.deltaTime * Speed * Curve.Evaluate((Time.time - time) / EasingTime);

                trajectory = raw.magnitude < trajectory.magnitude ? raw : trajectory;
                transform.position += trajectory;
                yield return null;                    
            }

            // TODO: Call to pickup gatherer thing
            Pool.DelayedDestroyGo(gameObject);
            yield return null;
        }
    }
}
