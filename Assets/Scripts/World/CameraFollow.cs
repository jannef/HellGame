using System.Collections;
using System.Collections.Generic;
using System.Linq;
using fi.tamk.hellgame.utils;
using UnityEngine;

namespace fi.tamk.hellgame.world
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Vector3 _offset;
        [SerializeField] private float smoothTime = 0.3F;
        [SerializeField] private Transform[] _corners;
        [SerializeField] private float CheatSlider = 30f;

        private Vector3 velocity = Vector3.zero;
        private Transform _cameraHolderTransform;

        private List<CameraInterest> _cameraInterestsRaw = new List<CameraInterest>();

        private float[] _camLimits = new float[4]; 

        private float _weightSum = 0f;
        private Vector3 CameraPosition
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
            _cameraHolderTransform.position = _offset;

            // Transformation
            var height = _cameraHolderTransform.position.y - _corners[0].position.y;
            var fowSize = height * Mathf.Tan(Camera.main.fieldOfView);

            _camLimits[0] = Mathf.Min(_corners[0].position.x, _corners[1].position.x) + CheatSlider * fowSize;
            _camLimits[1] = Mathf.Max(_corners[0].position.x, _corners[1].position.x) - CheatSlider * fowSize;
            _camLimits[2] = Mathf.Min(_corners[0].position.z, _corners[1].position.z) + CheatSlider * fowSize / Camera.main.aspect;
            _camLimits[3] = Mathf.Max(_corners[0].position.z, _corners[1].position.z) - CheatSlider * fowSize / Camera.main.aspect;

            if (_camLimits[0] > _camLimits[1])
            {
                var middlePoint = (_camLimits[0] + _camLimits[1]) / 2;

                _camLimits[0] = middlePoint;
                _camLimits[1] = middlePoint;
            }

            if (_camLimits[2] > _camLimits[3])
            {
                var middlePoint = (_camLimits[2] + _camLimits[3]) / 2;

                _camLimits[2] = middlePoint;
                _camLimits[3] = middlePoint;
            }

            ServiceLocator.WorldLimits = new [] {
                Mathf.Min(_corners[0].position.x, _corners[1].position.x), 
                Mathf.Max(_corners[0].position.x, _corners[1].position.x), 
                Mathf.Min(_corners[0].position.z, _corners[1].position.z),
                Mathf.Max(_corners[0].position.z, _corners[1].position.z)
            };
        }

        [ExecuteInEditMode]
        protected void LateUpdate()
        {
            var pos = new Vector3(
                Mathf.Clamp((CameraPosition + _offset).x, _camLimits[0], _camLimits[1]),
                _offset.y,
                Mathf.Clamp((CameraPosition + _offset).z, _camLimits[2], _camLimits[3]));

            if (_cameraInterestsRaw.Count > 0) _cameraHolderTransform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, smoothTime);
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

        public void PlaceParticleEffectInfrontOfCamera(Transform effectTransform, float distanceFromCamera)
        {
            effectTransform.position = Camera.main.transform.position + Camera.main.transform.forward * distanceFromCamera;
            effectTransform.forward = -Camera.main.transform.forward;
            effectTransform.SetParent(Camera.main.transform);   
        }
    }
}
