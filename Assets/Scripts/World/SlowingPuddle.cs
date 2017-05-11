using fi.tamk.hellgame.character;
using fi.tamk.hellgame.dataholders;
using fi.tamk.hellgame.utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.world
{

    public class SlowingPuddle : MonoBehaviour
    {
        private bool _playerIsInside;
        [SerializeField] private ParticleSystem _playerWalkIndicator;

        private void OnTriggerEnter(Collider other)
        {
            if (!_playerIsInside)
            {
                _playerIsInside = true;
                SlowDownPlayer(other.gameObject);
            }
        }

        private void SlowDownPlayer(GameObject go)
        {
            var actor = go.GetComponent<ActorComponent>();
            if (actor != null)
            {
                actor.ActorNumericData.ActorFloatData[(int)ActorDataMap.Speed] = Constants.PlayerRunningSpeed * Constants.PuddleSlowDownRate;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (!_playerIsInside)
            {
                _playerIsInside = true;
                SlowDownPlayer(other.gameObject);
            }

            if (_playerWalkIndicator != null)
            {
                _playerWalkIndicator.transform.position = new Vector3(other.transform.position.x, _playerWalkIndicator.transform.position.y, other.transform.position.z);
                _playerWalkIndicator.Play();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (_playerIsInside)
            {
                _playerIsInside = false;
                if (_playerWalkIndicator != null) _playerWalkIndicator.Stop();
            }

            var actor = other.gameObject.GetComponent<ActorComponent>();
            if (actor != null)
            {
                actor.ActorNumericData.ActorFloatData[(int)ActorDataMap.Speed] = Constants.PlayerRunningSpeed;
            }
        }
    }
}
