using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace fi.tamk.hellgame.world
{

    public class TriggerEvents : MonoBehaviour
    {
        private bool _playerIsInside;
        public UnityEvent PlayerEnters;
        public UnityEvent PlayerExits;

        private void OnTriggerEnter(Collider other)
        {
            if (!_playerIsInside)
            {
                _playerIsInside = true;
                if (PlayerEnters != null) PlayerEnters.Invoke();
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (!_playerIsInside)
            {
                _playerIsInside = true;
                if (PlayerEnters != null) PlayerEnters.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (_playerIsInside)
            {
                _playerIsInside = false;
                if (PlayerExits != null) PlayerExits.Invoke();
            }
        }
    }
}
