using fi.tamk.hellgame.world;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace fi.tamk.hellgame.character
{
    class PassiveTurret : MonoBehaviour
    {
        private BulletEmitter[] emitters;
        private List<int> gunsToFire = new List<int>();
        private Transform _targetTransform;
        private Quaternion _startingRotation;
        [SerializeField] float _turningSpeed;

        void Start()
        {
            emitters = GetComponentsInChildren<BulletEmitter>();
            _startingRotation = transform.rotation;
        }

        public void AddGunToFiringList(bool clearOthers, params int[] gunToAdd)
        {
            if (clearOthers) gunsToFire.Clear();

            gunsToFire.AddRange(gunToAdd.Where(x => !gunsToFire.Contains(x)));
        }

        public void AimAtTransform(Transform target)
        {
            _targetTransform = target;
        }

        public void StopAiming()
        {
            _targetTransform = null;
            transform.rotation = _startingRotation;

        }

        private void Aim(Vector3 position)
        {
            transform.forward = Vector3.RotateTowards(transform.forward,
                    new Vector3(position.x, transform.position.y,
                    position.z) - transform.position,
                    _turningSpeed * WorldStateMachine.Instance.DeltaTime, 0.0f);
        }

        void Update()
        {
            if (_targetTransform != null)
            {
                Aim(_targetTransform.position);
            }

            foreach (int i in gunsToFire)
            {
                if (emitters.Length > i)
                {
                    emitters[i].Fire();
                } 
                
            }
        }
    }
}
