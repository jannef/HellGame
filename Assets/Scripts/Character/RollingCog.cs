using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.character
{

    public class RollingCog : MonoBehaviour
    {

        public enum RotationAxis
        {
            X, Y, Z
        }

        private Vector3 _previousPosition;
        private Transform _transform;
        [SerializeField]
        private float _radius = 1;
        [SerializeField]
        RotationAxis _movementAxis;
        [SerializeField]
        Vector3 _rotationAxis;
        private float _circumcenter;

        // Use this for initialization
        void Start()
        {
            _transform = GetComponent<Transform>();
            _previousPosition = _transform.position;
            _circumcenter = _radius * 2 * Mathf.PI;
        }

        // Update is called once per frame
        void Update()
        {
            var movedAmount = 0f;

            switch (_movementAxis)
            {
                case RotationAxis.Z:
                    movedAmount = _previousPosition.z - transform.position.z;
                    if (movedAmount != 0)
                    {
                        _transform.Rotate(_rotationAxis * 360 * (movedAmount / _circumcenter));
                        _previousPosition = _transform.position;
                    }
                    break;
                case RotationAxis.X:
                    movedAmount = _previousPosition.x - transform.position.x;
                    
                    if (movedAmount != 0)
                    {
                        _transform.Rotate(_rotationAxis * 360 * (movedAmount / _circumcenter));
                        _previousPosition = _transform.position;
                    }
                    break;
                case RotationAxis.Y:
                    movedAmount = _previousPosition.y - transform.position.y;

                    if (movedAmount != 0)
                    {
                        _transform.Rotate(_rotationAxis * 360 * (movedAmount / _circumcenter));
                        _previousPosition = _transform.position;
                    }
                    break;
            }


        }
    }
}
