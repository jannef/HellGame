using System.Collections;
using System.Collections.Generic;
using System.Linq;
using fi.tamk.hellgame.utils;
using UnityEngine;

namespace fi.tamk.hellgame.world
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private float smoothTime = 0.3F;

        private Vector3 velocity = Vector3.zero;
        private Transform _cameraHolderTransform;

        private List<CameraInterest> _cameraInterestsRaw = new List<CameraInterest>();

        private float _weightSum = 0f;
        private Vector3 _cameraPosition
        {
            get
            {
                Vector3 vec = Vector3.zero;
                foreach (var i in _cameraInterestsRaw)
                {
                    vec += i.InterestTransform.position * i.InterestWeight;
                }

                return vec / _weightSum;
            }
        }

        protected void Awake()
        {
            _cameraHolderTransform = GetComponent<Transform>();
            _cameraHolderTransform.position = _playerTransform.position + _offset;
        }

        [ExecuteInEditMode]
        protected void LateUpdate()
        {
            if (_cameraInterestsRaw.Count > 0) _cameraHolderTransform.position = Vector3.SmoothDamp(transform.position, _cameraPosition + _offset, ref velocity, smoothTime);
        }

        public void AddInterest(CameraInterest newInterest)
        {
            if (_cameraInterestsRaw.Count(x => x.InterestTransform == newInterest.InterestTransform) > 0)
            {
                _cameraInterestsRaw.RemoveAll(x => x.InterestTransform == newInterest.InterestTransform);
            }
            _cameraInterestsRaw.Add(newInterest);
            _weightSum += newInterest.InterestWeight;
        }

        public void RemoveInterest(Transform whichInterest)
        {
            _cameraInterestsRaw = _cameraInterestsRaw.Where(x => x.InterestTransform != whichInterest).ToList();
            _weightSum = _cameraInterestsRaw.Sum(X => X.InterestWeight);
        }
    }
}
