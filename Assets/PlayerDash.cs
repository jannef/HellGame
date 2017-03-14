using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.character
{

    public class PlayerDash : MonoBehaviour
    {

        [SerializeField] private ParticleSystem _playerTrail;

        public void StartDash()
        {
            _playerTrail.Play();
        }

        public void StopDash()
        {
            _playerTrail.Stop();
        }
    }
}
