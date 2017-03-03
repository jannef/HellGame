using System.Collections;
using System.Collections.Generic;
using fi.tamk.hellgame.world;
using UnityEngine;

namespace fi.tamk.hellgame.effects
{

    public class LaserPointer : MonoBehaviour
    {
        [SerializeField] Transform _bulletOrigin;
        [SerializeField] float _laserMinLenght;
        [SerializeField] float _laserMaxLenght;
        [SerializeField] private LayerMask _hittingLayer;
        public float ChargeTime = 1f;

        public AnimationCurve LaserEasingCurve;
        private float _chargeTimer;
        private LineRenderer _laserRenderer;

        protected Vector3 GunVector
        {
            get { return (_bulletOrigin.position - transform.position).normalized; }
        }

        // Use this for initialization
        void Awake()
        {
            _laserRenderer = GetComponent<LineRenderer>();
        }

        public void Initialize(AnimationCurve laserEasingCurve, float chargeMaxLenght)
        {
            this.LaserEasingCurve = laserEasingCurve;
            ChargeTime = chargeMaxLenght;
        }

        public void Activate()
        {
            _chargeTimer = 0f;
            _laserRenderer.enabled = true;
            _laserRenderer.SetPosition(0, transform.position);
            _laserRenderer.SetPosition(1, transform.position);
            enabled = true;
        }

        public void Disable()
        {
            enabled = false;
            _laserRenderer.enabled = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (_chargeTimer < ChargeTime) _chargeTimer += WorldStateMachine.Instance.DeltaTime;
            ;

            var ray = new Ray(transform.position, GunVector);
            var maxDistance = Mathf.Lerp(_laserMinLenght, _laserMaxLenght,
                LaserEasingCurve.Evaluate(_chargeTimer / ChargeTime));
            RaycastHit hit;
            if (Physics.Raycast(ray.origin, ray.direction, out hit, maxDistance, _hittingLayer))
            {
                _laserRenderer.SetPosition(0, transform.position);
                _laserRenderer.SetPosition(1, hit.point);
            }
            else
            {
                _laserRenderer.SetPosition(0, transform.position);
                _laserRenderer.SetPosition(1, transform.position + (GunVector * maxDistance));
            }

        }
    }
}
