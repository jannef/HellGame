using fi.tamk.hellgame.character;
using fi.tamk.hellgame.utils;
using fi.tamk.hellgame.world;
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
    [SerializeField] protected LayerMask stoppingLayer;

    [SerializeField, Range(0f, 10f)] protected float WarningLenght;
    [SerializeField, Range(0.06f, 10f)] protected float LaserLenght;

    public object Servicelocator { get; private set; }

    public override void Fire()
    {
        if (!(Timer > Cooldown)) return;

        StartCoroutine(LaserRoutine());

        Timer = 0f;
    }

    protected override void Awake()
    {
        Timer = 0f;
    }

    private IEnumerator LaserRoutine()
    {
        float time = 0;
        Vector3[] laserPositions = new Vector3[2];
        var width = 0f;
        ShotLaserRenderer.enabled = true;

        while (time < WarningLenght)
        {
            var ray = new Ray(transform.position, GunVector);
            RaycastHit[] hitData = Physics.RaycastAll(ray.origin, ray.direction, 1000f, stoppingLayer).OrderBy(h => h.distance).ToArray();

            foreach (RaycastHit hit in hitData)
            {

                if (stoppingLayer == (stoppingLayer | (1 << hit.collider.gameObject.layer)))
                {
                    //laserPositions[1] = hit.point;
                    
                    break;
                }
            }

            laserPositions[0] = transform.position;
            laserPositions[1] = transform.position + GunVector * 100f;
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

        while (time < LaserLenght)
        { 
            var ray = new Ray(transform.position, GunVector);
            RaycastHit[] hitData = Physics.RaycastAll(ray.origin, ray.direction, 100f, FireAtWhichLayer).OrderBy(h => h.distance).ToArray();
            
            

            foreach (RaycastHit hit in hitData)
            {
                var hc = Pool.Instance.GetHealthComponent(hit.collider.gameObject);

                if (hc != null)
                {
                    hc.TakeDamage(1);
                }

                if (stoppingLayer == (stoppingLayer | (1 << hit.collider.gameObject.layer)))
                {
                    //laserPositions[1] = hit.point;

                    break;
                }
            }

            if (FiringEvent != null) FiringEvent.Invoke();

            laserPositions[0] = transform.position;
            laserPositions[1] = transform.position + GunVector * 100f;
            ShotLaserRenderer.SetPositions(laserPositions);


            time += WorldStateMachine.Instance.DeltaTime;
            yield return null;
        }

        time = shotEndLenght;

        while (time > 0)
        {
            width = Mathf.Lerp(warningShotWidth, fullShotWidth, warningToShotEasing.Evaluate(time / shotEndLenght));
            ShotLaserRenderer.startWidth = width;
            ShotLaserRenderer.endWidth = width;

            laserPositions[0] = transform.position;
            laserPositions[1] = transform.position + GunVector * 100f;
            ShotLaserRenderer.SetPositions(laserPositions);

            time -= WorldStateMachine.Instance.DeltaTime;
            yield return null;
        }

        ShotLaserRenderer.enabled = false;

    }
}
