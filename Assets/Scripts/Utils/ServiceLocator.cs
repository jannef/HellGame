using System.Collections.Generic;
using System.Linq;
using fi.tamk.hellgame.world;
using UnityEngine;

namespace fi.tamk.hellgame.utils
{
    public class ServiceLocator : Singleton<ServiceLocator>
    {
        private readonly List<Transform> _players = new List<Transform>();
        private CameraFollow _mainCameraFollow;

        public CameraFollow MainCameraScript
        {
            get { return _mainCameraFollow ?? (_mainCameraFollow = FindObjectOfType<CameraFollow>()); }
        }

        public void RegisterPlayer(Transform playerTransform)
        {
            if (_players.Contains(playerTransform)) return;

            _players.Add(playerTransform);
        }

        public void UnregisterPlayer(Transform whichPlayer)
        {
            if (_players.Contains(whichPlayer)) _players.Remove(whichPlayer);
        }

        public Transform GetNearestPlayer(Vector3 requerPosition)
        {
            if (Quitting) return null;
            return _players.Count < 1 ? null : _players.OrderBy(t => (t.position - requerPosition).magnitude).First();
        }
    }
}
