using fi.tamk.hellgame.character;
using fi.tamk.hellgame.utils;
using fi.tamk.hellgame.world;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class LaserEmitter : BulletEmitter {

    [SerializeField] protected LineRenderer ShotLaserRenderer;
    [SerializeField] private float warningShotWidth;
    [SerializeField] private float fullShotWidth;
    [SerializeField] private float shotEndLenght;
    [SerializeField] private AnimationCurve warningToShotEasing;
    [SerializeField] private ParticleSystem _endPointParticle;
    [SerializeField] private ParticleSystem _rootParticles;

    [SerializeField, Range(0f, 10f)] protected float WarningLenght;
    [SerializeField, Range(0.06f, 10f)] protected float LaserLenght;
    [SerializeField, Range(0.01f, 1f)] protected float LaserColliderWidthMultiplier;
    [FMODUnity.EventRef]
    public String LaserSoundEffect = "";
    private FMOD.Studio.EventInstance _laserSound;

    private CapsuleCollider _laserCollider;
    private bool coroutineRunning = false;

    private bool StopFiringLaser
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
        set
        {
            _stopFiringLaser = value;
        }
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
        if (!string.IsNullOrEmpty(LaserSoundEffect)) {
            _laserSound = FMODUnity.RuntimeManager.CreateInstance(LaserSoundEffect);
            var attributes = FMODUnity.RuntimeUtils.To3DAttributes(transform);
            _laserSound.set3DAttributes(attributes);
        }
    }

    private Vector3[] FindLaserPositions(Vector3[] positions)
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

    private void StartLaserSound()
    {
        if (_laserSound == null) return;
        _laserSound.setParameterValue("LaserState", 1f);
        _laserSound.start();
    }

    private void StopLaserSound()
    {
        if (_laserSound != null)
        {
            _laserSound.setParameterValue("LaserState", 0f);
        }
        
    }

    private void OnDestroy()
    {
        if (_laserSound != null) _laserSound.release();
    }

    private IEnumerator LaserRoutine(bool endWithTime = false)
    {
        coroutineRunning = true;
        float time = 0;
        Vector3[] laserPositions = new Vector3[2];
        var width = 0f;
        ShotLaserRenderer.enabled = true;
        ShotLaserRenderer.startWidth = warningShotWidth;
        ShotLaserRenderer.endWidth = fullShotWidth;
        StartLaserSound();

        while (time < WarningLenght)
        {
            laserPositions = FindLaserPositions(laserPositions);
           
            ShotLaserRenderer.SetPositions(laserPositions);
            width = Mathf.Lerp(warningShotWidth, fullShotWidth, warningToShotEasing.Evaluate(time / WarningLenght));
            ShotLaserRenderer.startWidth = width;
            ShotLaserRenderer.endWidth = width;


            time += WorldStateMachine.Instance.DeltaTime;
            yield return null;
        }

        time = 0;
        width = Mathf.Lerp(warningShotWidth, fullShotWidth, warningToShotEasing.Evaluate(1));
        ShotLaserRenderer.startWidth = width;
        ShotLaserRenderer.endWidth = width;
        _laserCollider.enabled = true;
        if (_endPointParticle != null) _endPointParticle.Play();
        if (_rootParticles != null) _rootParticles.Play();

        while (time < LaserLenght && !StopFiringLaser)
        {
            laserPositions = FindLaserPositions(laserPositions);
            if (_endPointParticle != null) _endPointParticle.transform.position = laserPositions[1];
            SetDamagingCollider(width, laserPositions[0], laserPositions[1]);

            if (FiringEvent != null) FiringEvent.Invoke();
            ShotLaserRenderer.SetPositions(laserPositions);


            if (endWithTime) {
                time += WorldStateMachine.Instance.DeltaTime;
            }
                
            yield return null;
        }

        _laserCollider.enabled = false;
        if (_endPointParticle != null) _endPointParticle.Stop();
        if (_rootParticles != null) _rootParticles.Stop();
        time = shotEndLenght;
        StopLaserSound();

        while (time > 0)
        {
            width = Mathf.Lerp(warningShotWidth, fullShotWidth, warningToShotEasing.Evaluate(time / shotEndLenght));
            ShotLaserRenderer.startWidth = width;
            ShotLaserRenderer.endWidth = width;

            laserPositions = FindLaserPositions(laserPositions);

            ShotLaserRenderer.SetPositions(laserPositions);

            time -= WorldStateMachine.Instance.DeltaTime;
            yield return null;
        }

        ShotLaserRenderer.enabled = false;
        coroutineRunning = false;
    }

    private void SetDamagingCollider(float width, Vector3 startPoint, Vector3 endPoint)
    {
        var direction = endPoint - startPoint;
        _laserCollider.transform.position = startPoint + (direction) / 2;
        _laserCollider.transform.up = direction;
        _laserCollider.height = direction.magnitude;
        _laserCollider.radius = (width / 2) * LaserColliderWidthMultiplier;
    }

    public override void DetachBulletEmitter(Vector3 localScale)
    {
    }
}
