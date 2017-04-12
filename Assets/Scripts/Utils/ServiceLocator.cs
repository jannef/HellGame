using System.Collections.Generic;
using System.Linq;
using fi.tamk.hellgame.world;
using fi.tamk.hellgame.character;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace fi.tamk.hellgame.utils
{
    public class ServiceLocator : Singleton<ServiceLocator>
    {
        private struct PlayerData
        {
            public Transform Transform;
            public PickupGathererComponent PickupComponent;

            public PlayerData(Transform transform, PickupGathererComponent pickupComponent)
            {
                Transform = transform;
                PickupComponent = pickupComponent;
            }
        }
        public bool RoomBeaten = false;
        private readonly List<PlayerData> _players = new List<PlayerData>();
        private CameraFollow _mainCameraFollow;
        public static float[] WorldLimits;

        public CameraFollow MainCameraScript
        {
            get { return (_mainCameraFollow ?? (_mainCameraFollow = FindObjectOfType<CameraFollow>())) ?? new UnityException("Main camera script not found in the scene!").Throw<CameraFollow>(); }
        }

        public RoomManager RoomManagerReference
        {
            get { return _roomManager ?? (_roomManager = FindObjectOfType<RoomManager>());}
        }
        private RoomManager _roomManager;

        public void RegisterPlayer(GameObject player)
        {
            if (_players.Count(x => x.Transform == player.transform) > 0) return;

            _players.Add(new PlayerData(player.transform, player.GetComponent<PickupGathererComponent>()));
        }

        public void UnregisterPlayer(GameObject player)
        {
            if (_players.Count(x => x.Transform = player.transform) > 0) _players.RemoveAll(x => x.Transform == player.transform);
        }

        public Transform GetNearestPlayer(Vector3 requerPosition)
        {
            if (Quitting) return null;
            return _players.Count < 1 ? null : _players.OrderBy(t => (t.Transform.position - requerPosition).sqrMagnitude).Last().Transform;
        }

        public Transform GetNearestPlayer()
        {
            return GetNearestPlayer(Vector3.zero);
        }

        public GameObject[] GetAllPlayerGameObjects()
        {
            if (Quitting) return new GameObject[0];
            return _players.Select(x => x.Transform.gameObject as GameObject).ToArray();
        }

        public PickupGathererComponent GetPickupGatherer(Transform playerOfWhich)
        {
            return _players.DefaultIfEmpty(new PlayerData(null, null)).First(x => x.Transform = playerOfWhich).PickupComponent;
        }
    }
}
