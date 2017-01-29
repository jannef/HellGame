using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.world
{
    public class CameraFollow : MonoBehaviour
    {
        private Transform _cameraHolderTransform;
        [SerializeField]
        private Transform _playerTransform;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private float smoothTime = 0.3F;
        private Vector3 velocity = Vector3.zero;

        void Awake()
        {
            _cameraHolderTransform = GetComponent<Transform>();
            _cameraHolderTransform.position = _playerTransform.position + _offset;
        }

        void LateUpdate()
        {
            _cameraHolderTransform.position = Vector3.SmoothDamp(transform.position, _playerTransform.position + _offset, ref velocity, smoothTime);
        }
    }
}
