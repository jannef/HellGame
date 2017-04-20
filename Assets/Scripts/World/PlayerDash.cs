using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.character
{

    public class PlayerDash : MonoBehaviour
    {

        [SerializeField] private ParticleSystem _playerTrail;
        [SerializeField] private ParticleSystem _playerParticle;

        public void StartDash()
        {
            _playerTrail.Play();
            _playerParticle.Play();
        }

        public void StopDash()
        {
            _playerTrail.Stop();
            _playerParticle.Stop();
        }
    }
}
