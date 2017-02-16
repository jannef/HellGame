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
            _cameraHolderTransform.position = _playerTransform.position + _offset;

            // Transformation
            var height = _cameraHolderTransform.position.y - _corners[0].position.y;
            var adjustment = height * Mathf.Tan(90 - Camera.main.transform.rotation.eulerAngles.x);

            var hyp = Mathf.Sqrt(Mathf.Pow(adjustment, 2f) + Mathf.Pow(height, 2f));
            var fowSize = hyp * Mathf.Tan(Camera.main.fieldOfView);

            Debug.Log(fowSize);

            _camLimits[0] = Mathf.Min(_corners[0].position.x, _corners[1].position.x) + 2 * fowSize;
            _camLimits[1] = Mathf.Max(_corners[0].position.x, _corners[1].position.x) - 2 * fowSize;
            _camLimits[2] = Mathf.Min(_corners[0].position.z, _corners[1].position.z) - adjustment + (Camera.main.aspect / fowSize) + CheatSlider;
            _camLimits[3] = Mathf.Max(_corners[0].position.z, _corners[1].position.z) - adjustment - (Camera.main.aspect / fowSize);

            _corners[0].position = new Vector3(_camLimits[0], 0, _camLimits[2]);
            _corners[1].position = new Vector3(_camLimits[1], 0, _camLimits[3]);
        }

        [ExecuteInEditMode]
        protected void LateUpdate()
        {
            var pos = new Vector3(
                Mathf.Clamp((CameraPosition + _offset).x, _camLimits[0], _camLimits[1]),
                (CameraPosition + _offset).y,
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
    }
}
