using fi.tamk.hellgame.character;
using fi.tamk.hellgame.utils;
using fi.tamk.hellgame.world;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

namespace fi.tamk.hellgame.character
{
    public class LaserEmitter : BulletEmitter
    {
        public UnityEvent AtFireStart;
        public UnityEvent AtFireEnd;

        [SerializeField] protected LineRenderer ShotLaserRenderer;
        [SerializeField] protected float warningShotWidth;
        [SerializeField] protected float fullShotWidth;
        [SerializeField] protected float shotEndLenght;
        [SerializeField] protected AnimationCurve warningToShotEasing;
        [SerializeField] protected ParticleSystem _endPointParticle;
        [SerializeField] protected ParticleSystem _rootParticles;

        [SerializeField, Range(0f, 10f)] protected float WarningLenght;
        [SerializeField, Range(0.06f, 10f)] protected float LaserLenght;
        [SerializeField, Range(0.01f, 1f)] protected float LaserColliderWidthMultiplier;
        [FMODUnity.EventRef] public String LaserSoundEffect = "";
        protected FMOD.Studio.EventInstance _laserSound;

        protected CapsuleCollider _laserCollider;
        protected bool coroutineRunning = false;

        protected MethodInfo BeamParticlesSetter;
        protected MethodInfo EmissionRateSetter;
        protected float EmissionRate;

        protected virtual void SetBeamEmission(float beamLenght)
        {
            var box = _rootParticles.shape.box;
            box.z = beamLenght;
            BeamParticlesSetter.Invoke(_rootParticles.shape, new object[] {box});
            EmissionRateSetter.Invoke(_rootParticles.emission, new object[] {EmissionRate * beamLenght});
        }

        protected bool StopFiringLaser
        {
            get
            {
                if (_stopFiringLaser)
                {
                    _stopFiringLaser = false;
                    return true;
                }

                return false;
            }
            set { _stopFiringLaser = value; }
        }

        private bool _stopFiringLaser = false;

        public object Servicelocator { get; private set; }

        public override void Fire()
        {
            if (!(Timer > Cooldown)) return;

            if (coroutineRunning)
            {
                StopAllCoroutines();
            }

            StartCoroutine(LaserRoutine(true));

            Timer = 0f;
        }

        public Action FireUntilFurtherNotice()
        {
            if (!(Timer > Cooldown)) return null;

            _stopFiringLaser = false;

            if (coroutineRunning)
            {
                StopAllCoroutines();
            }

            StartCoroutine(LaserRoutine());

            Timer = 0f;
            return StopFiring;
        }

        public void StopFiring()
        {
            _stopFiringLaser = true;
        }

        protected override void Awake()
        {
            Timer = 0f;
            _laserCollider = GetComponentInChildren<CapsuleCollider>();
            if (!string.IsNullOrEmpty(LaserSoundEffect))
            {
                _laserSound = FMODUnity.RuntimeManager.CreateInstance(LaserSoundEffect);
                var attributes = FMODUnity.RuntimeUtils.To3DAttributes(transform);
                _laserSound.set3DAttributes(attributes);
            }

            BeamParticlesSetter = typeof(ParticleSystem.ShapeModule).GetMethod("set_box");
            EmissionRateSetter = typeof(ParticleSystem.EmissionModule).GetMethod("set_rateOverTimeMultiplier");
            EmissionRate = _rootParticles.emission.rateOverTimeMultiplier / _rootParticles.shape.box.z;
        }

        protected virtual Vector3[] FindLaserPositions(ref Vector3[] positions)
        {
            var ray = new Ray(transform.position, GunVector);
            RaycastHit hitData;
            if (Physics.Raycast(ray.origin, ray.direction, out hitData, 100f, FireAtWhichLayer))
            {
                positions[1] = hitData.point;
            }
            else
            {
                positions[1] = transform.position + GunVector * 100f;
            }

            positions[0] = transform.position;

            return positions;
        }

        protected virtual void StartLaserSound()
        {
            if (_laserSound == null) return;
            _laserSound.start();
        }

        protected virtual void StopLaserSound()
        {
            if (_laserSound != null)
            {
                _laserSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            }
        }

        protected virtual void OnDestroy()
        {
            if (_laserSound != null)
            {
                _laserSound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                _laserSound.release();
            }
        }

        protected IEnumerator LaserRoutine(bool endWithTime = false)
        {
            coroutineRunning = true;
            float time = 0;
            Vector3[] laserPositions = new Vector3[2];
            var width = 0f;
            ShotLaserRenderer.enabled = true;
            ShotLaserRenderer.startWidth = warningShotWidth;
            ShotLaserRenderer.endWidth = fullShotWidth;
            StartLaserSound();
            if (AtFireStart != null) AtFireStart.Invoke();

            while (time < WarningLenght)
            {
                FindLaserPositions(ref laserPositions);

                ShotLaserRenderer.SetPositions(laserPositions);
                width = Mathf.Lerp(warningShotWidth, fullShotWidth, warningToShotEasing.Evaluate(time / WarningLenght));
                ShotLaserRenderer.startWidth = width;
                ShotLaserRenderer.endWidth = width;


                time += WorldStateMachine.Instance.DeltaTime;
                yield return null;
            }

            time = 0;
            ShotLaserRenderer.startWidth = fullShotWidth;
            ShotLaserRenderer.endWidth = fullShotWidth;
            _laserCollider.enabled = true;
            if (_endPointParticle != null) _endPointParticle.Play();
            if (_rootParticles != null) _rootParticles.Play();

            while (time < LaserLenght && !StopFiringLaser)
            {
                FindLaserPositions(ref laserPositions);
                
                SetEndParticles(laserPositions);
                SetDamagingCollider(width, laserPositions[0], laserPositions[1]);

                if (FiringEvent != null) FiringEvent.Invoke();
                ShotLaserRenderer.SetPositions(laserPositions);

                if (endWithTime)
                {
                    time += WorldStateMachine.Instance.DeltaTime;
                }

                yield return null;
            }

            _laserCollider.enabled = false;
            if (_endPointParticle != null) _endPointParticle.Stop();
            if (_rootParticles != null) _rootParticles.Stop();
            time = shotEndLenght;
            StopLaserSound();
            if (AtFireEnd != null) AtFireEnd.Invoke();

            while (time > 0)
            {
                width = Mathf.Lerp(warningShotWidth, fullShotWidth, warningToShotEasing.Evaluate(time / shotEndLenght));
                ShotLaserRenderer.startWidth = width;
                ShotLaserRenderer.endWidth = width;

                FindLaserPositions(ref laserPositions);
                ShotLaserRenderer.SetPositions(laserPositions);

                time -= WorldStateMachine.Instance.DeltaTime;
                yield return null;
            }

            ShotLaserRenderer.enabled = false;
            coroutineRunning = false;
        }

        protected virtual void SetEndParticles(Vector3[] laserPositions)
        {
            if (_endPointParticle != null)
            {
                _endPointParticle.transform.position = laserPositions[1];
            }
        }

        protected virtual void SetDamagingCollider(float width, Vector3 startPoint, Vector3 endPoint)
        {
            var direction = endPoint - startPoint;
            var middlePoint = startPoint + (direction / 2f);
            _laserCollider.transform.position = middlePoint;
            _rootParticles.transform.position = middlePoint;
            _laserCollider.transform.up = direction;
            var len = direction.magnitude;
            _laserCollider.height = len;
            _laserCollider.radius = (width / 2) * LaserColliderWidthMultiplier;
            SetBeamEmission(len);
        }

        public override void DetachBulletEmitter(Vector3 localScale)
        {
        }
    }
}
