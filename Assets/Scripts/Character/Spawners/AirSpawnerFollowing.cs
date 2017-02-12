using fi.tamk.hellgame.character;
using fi.tamk.hellgame.utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.character
{

    public class AirSpawnerFollowing : AirSpawnerWithSetSpawnPoints
    {
        [SerializeField] private Transform targetTransform;
        private float secondRequestTimer;
        private float delayBetweenRequests = 1f;

        void Start()
        {
            if (targetTransform == null) targetTransform = ServiceLocator.Instance.GetNearestPlayer(transform.position);
        }

        void Update()
        {
            if (targetTransform != null)
            {
                transform.position = targetTransform.position;
            } else
            {
                secondRequestTimer += Time.deltaTime;
                if (secondRequestTimer > delayBetweenRequests)
                {
                    secondRequestTimer = 0;
                    targetTransform = ServiceLocator.Instance.GetNearestPlayer(transform.position);
                }
            }
        }
    }
}
