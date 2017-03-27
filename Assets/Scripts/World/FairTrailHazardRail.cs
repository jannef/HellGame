using System.Collections;
using System.Collections.Generic;
using fi.tamk.hellgame.utils;
using UnityEngine;

namespace fi.tamk.hellgame.world
{
    public class FairTrailHazardRail : MonoBehaviour
    {
        // TODO: pool these
        [SerializeField] private GameObject HazardPrefab;
        [Range(0.1f, 20f), SerializeField] private float FlameSpeed;
        [Range(1f, 4f), SerializeField] private float IndicatorSpeedMultiplier;
        [SerializeField] private AnimationCurve _speedInterpolation;
        [SerializeField] private Transform _start;
        [SerializeField] private Transform _end;


        [ExecuteInEditMode]
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(_start.position, _end.position);
        }

        public void PlayTheFlame(bool startToEnd = true)
        {
            StartCoroutine(FireTrailRun(GetHazardObject()));
        }

        public FireHazard GetHazardObject()
        {
            // TODO: Get from pool instead
            return Instantiate(HazardPrefab).GetComponent<FireHazard>() ?? new UnityException(string.Format("FireHazard prefab {0} does not have FireHazard component!", HazardPrefab)).Throw<FireHazard>();
        }

        private IEnumerator FireTrailRun(FireHazard hazardObject)
        {
            var trajectory = (_end.position - _start.position);
            var duration = trajectory.magnitude / FlameSpeed;
            var timer = 0f;
            var trans = hazardObject.transform;
            var indic = hazardObject.IndicatorGameObject.transform;
            var indicatorPlaying = true;
            hazardObject.StartHazard();
            while (timer < duration)
            {
                var ratio = timer / duration;
                timer += WorldStateMachine.Instance.DeltaTime;
                trans.position = Vector3.Lerp(_start.position, _end.position, ratio);

                if (indicatorPlaying)
                {
                    var iRat = ratio * IndicatorSpeedMultiplier;
                    indic.position = Vector3.Lerp(_start.position, _end.position, iRat);
                    if (iRat > 1f)
                    {
                        hazardObject.StopIndicator();
                        indicatorPlaying = false;
                    }
                }

                yield return null;
            }

            hazardObject.EndHazard();
            hazardObject.gameObject.SetActive(false);
            Destroy(hazardObject.gameObject);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                Debug.Log("Running FireHazard test.");
                PlayTheFlame();
            }
        }
    }
}
