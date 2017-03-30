using System.Collections;
using System.Collections.Generic;
using fi.tamk.hellgame.utils;
using UnityEngine;
using fi.tamk.hellgame.effects;
using System;

namespace fi.tamk.hellgame.world
{
    public class FairTrailHazardRail : MonoBehaviour
    {
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

        public void PlayTheFlame()
        {
            var trans = ServiceLocator.Instance.GetNearestPlayer(_start.position);
            var loc = trans != null ? trans.position : Vector3.zero;

            var playFromStart = (loc - _start.position).sqrMagnitude > (loc - _end.position).sqrMagnitude;

            var hazard = GetHazardObject();
            hazard.Caller = this;
            hazard.Controller = StartCoroutine(FireTrailRun(hazard, playFromStart));
        }

        internal void StopTrailCoroutine(Coroutine controller)
        {
            StopCoroutine(controller);
        }

        public FireHazard GetHazardObject()
        {
            return Pool.Instance.GetObject(HazardPrefab).GetComponent<FireHazard>();
        }

        private IEnumerator FireTrailRun(FireHazard hazardObject, bool startToEnd)
        {
            var duration = (_end.position - _start.position).magnitude / FlameSpeed;
            var timer = 0f;
            var trans = hazardObject.transform;
            var indic = hazardObject.IndicatorGameObject.transform;
            var indicatorPlaying = true;
            hazardObject.StartHazard();
            while (timer < duration)
            {
                var ratio = timer / duration;
                timer += WorldStateMachine.Instance.DeltaTime;

                if (startToEnd)
                {
                    trans.position = Vector3.Lerp(_start.position, _end.position, ratio);
                }
                else
                {
                    trans.position = Vector3.Lerp(_end.position, _start.position, ratio);
                }

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
            var go = hazardObject.gameObject;
            Pool.Instance.ReturnObject(ref go);
        }


        private void OnDestroy()
        {
            StopAllCoroutines();
        }
    }    
}
