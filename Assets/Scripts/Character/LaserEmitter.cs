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

    [SerializeField, Range(0f, 10f)] protected float WarningLenght;
    [SerializeField, Range(0.06f, 10f)] protected float LaserLenght;

    private CapsuleCollider _laserCollider;
    private float _parentSizeModifier;
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
        _parentSizeModifier = transform.parent.localScale.x;
        _laserCollider = GetComponentInChildren<CapsuleCollider>();
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

    private IEnumerator LaserRoutine(bool endWithTime = false)
    {
        coroutineRunning = true;
        float time = 0;
        Vector3[] laserPositions = new Vector3[2];
        var width = 0f;
        ShotLaserRenderer.enabled = true;
        ShotLaserRenderer.startWidth = warningShotWidth;
        ShotLaserRenderer.endWidth = fullShotWidth;

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

        while (time < LaserLenght && !StopFiringLaser)
        {
            laserPositions = FindLaserPositions(laserPositions);

            SetDamagingCollider(width, laserPositions[0], laserPositions[1]);

            if (FiringEvent != null) FiringEvent.Invoke();
            ShotLaserRenderer.SetPositions(laserPositions);


            if (endWithTime) {
                time += WorldStateMachine.Instance.DeltaTime;
            }

                
            yield return null;
        }

        _laserCollider.enabled = false;

        time = shotEndLenght;

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
        _laserCollider.height = direction.magnitude / _parentSizeModifier;
        _laserCollider.radius = width / 2 / _parentSizeModifier / 3;
    }

    public override void DetachBulletEmitter(Vector3 localScale)
    {
    }
}
