using fi.tamk.hellgame.world;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.effects
{
    public class FireHazard : MonoBehaviour
    {
        public GameObject IndicatorGameObject { get { return _indicator.gameObject; } }

        [SerializeField] private ParticleSystem _fire;
        [SerializeField] private ParticleSystem _indicator;

        public Coroutine Controller;
        public FairTrailHazardRail Caller;

        public void StartHazard()
        {
            _fire.Play();
            _indicator.Play();
        }

        public void EndHazard()
        {
            _fire.Pause();
            _indicator.Pause();
        }

        public void StopIndicator()
        {
            _indicator.Stop();
        }

        private void OnDisable()
        {
            if (Controller != null && Caller != null)
            {
                Caller.StopTrailCoroutine(Controller);
                Controller = null;
                Caller = null;
            }
        }
    }
}
