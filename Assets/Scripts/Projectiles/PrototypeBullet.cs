using System.Collections;
using System.Collections.Generic;
using fi.tamk.hellgame.utils.Stairs.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace fi.tamk.hellgame.projectiles
{
    public delegate void OnTickDelegate(); 

    [RequireComponent(typeof(Rigidbody))]
    public class PrototypeBullet : MonoBehaviour
    {
        public UnityEvent OnLifeTimeDeactivation;
        public UnityEvent OnImpact;

        public Vector3 Acceleration;
        public float Speed;
        public float LifeTime;
        public int Damage;

        private Vector3 _acceleration;
        private float _speed;
        private int _damage;
        private float _lifeTime;

        private float _timer;

        private Rigidbody _rigidbody;

        private void Update()
        {
            Speed += Acceleration.x;
            transform.Rotate(transform.up, -Acceleration.z * Time.deltaTime);
            _rigidbody.MovePosition(transform.position + transform.forward * Speed * Time.deltaTime);

            _timer += Time.deltaTime;
            if (_timer > LifeTime)
            {
                OnLifeTimeDeactivation.Invoke();
                Deactivate();
            }
        }

        protected void OnEnable()
        {
            Acceleration = _acceleration;
            Speed = _speed;
            Damage = _damage;
            LifeTime = _lifeTime;
            _timer = 0f;
        }

        private void Awake()
        {
            _acceleration = Acceleration;
            _speed = Speed;
            _damage = Damage;
            _lifeTime = LifeTime;
            _timer = 0f;

            _rigidbody = GetComponent<Rigidbody>();
        }

        protected void Deactivate()
        {
            var go = gameObject;
            Pool.Instance.ReturnObject(ref go);
        }
    }
}
