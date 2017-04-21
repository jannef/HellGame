using fi.tamk.hellgame.utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.character
{

    public class PlayerDash : MonoBehaviour
    {

        [SerializeField] private ParticleSystem _playerTrail;
        [SerializeField] private ParticleSystem _playerParticle;
        [FMODUnity.EventRef]
        public String DashSound = "";

        public void StartDash()
        {
            _playerTrail.Play();
            _playerParticle.Play();
            Utilities.PlayOneShotSound(DashSound, transform.position);
        }

        public void StopDash()
        {
            _playerTrail.Stop();
            _playerParticle.Stop();
        }
    }
}
