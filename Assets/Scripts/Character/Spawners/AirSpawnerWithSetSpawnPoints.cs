using System;
using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.utils;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using fi.tamk.hellgame.dataholders;
using fi.tamk.hellgame.world;
using Random = UnityEngine.Random;

namespace fi.tamk.hellgame.character
{

    public class AirSpawnerWithSetSpawnPoints : MonoBehaviour, ISpawner
    {
        [SerializeField] protected Transform SpawnPointsParent;
        [SerializeField] protected Vector3 AirDropOffset;
        private Transform[] _availableSpawnPoints;

        protected void Awake()
        {
            var transformArray = SpawnPointsParent.GetComponentsInChildren<Transform>();
            if (transformArray.Length <= 1)
            {
                _availableSpawnPoints = new [] { transform };
            }
            else
            {
                _availableSpawnPoints = new Transform[transformArray.Length - 1];

                // This is used to remove the parent from available spawnPoints.
                var availableSpawnPointIndex = 0;
                foreach (var t in transformArray)
                {
                    if (t.GetInstanceID() != SpawnPointsParent.GetInstanceID())
                    {
                        _availableSpawnPoints[availableSpawnPointIndex] = t;
                        availableSpawnPointIndex++;
                    }
                }
            }
        }

        public HealthComponent[] Spawn(SpawnerInstruction instructions)
        {
            if (instructions == null)
            {
                Debug.Log("AirSpawner: instructions are null");
                return null;
            }

            if (instructions.prefabToSpawn == null)
            {
                Debug.Log("AirSpawner: prefab to spawn is null");
                return null;
            }

            var returnMinionComponents = instructions.prefabToSpawn.GetComponent<HealthComponent>() != null;

            return SpawnObjects(instructions.prefabToSpawn, instructions.numberOfSpawns, instructions.delayBetweenSpawns,
                instructions.spawnAreaRandomness, returnMinionComponents, instructions.SpawnPointSpread);
        }

        private HealthComponent[] SpawnObjects(GameObject prefabToSpawn, int numberToSpawn, float delayBetweenSpawns, float spawnAreaSize, 
            bool returnMinionComponent, SpawnPointSpread spreadType)
        {
            if (prefabToSpawn == null) throw new NullReferenceException("PrefabToSpawn set to null!");

            var minionComponents = new HealthComponent[numberToSpawn];
            var spawnedObjects = new List<GameObject>();
            var spawnPointIndex = 0;

            if (spreadType == SpawnPointSpread.RandomEvenly)
            {
                var rnd = new System.Random();
                var myArray = _availableSpawnPoints;
                _availableSpawnPoints = myArray.OrderBy(x => rnd.Next()).ToArray();
            }

            for (var i = 0; i < numberToSpawn; i++)
            {
                Debug.Log(_availableSpawnPoints.Length);
                var targetSpawnPoint = _availableSpawnPoints.Length == 0 ? Vector3.zero : _availableSpawnPoints[spreadType == SpawnPointSpread.CompletelyRandom ? Random.Range(0, _availableSpawnPoints.Length - 1) : spawnPointIndex].position;
                spawnPointIndex = (spawnPointIndex + 1) % _availableSpawnPoints.Length;

                var ray = new Ray(AirDropOffset + targetSpawnPoint + Random.insideUnitSphere * spawnAreaSize, Vector3.down);

                if (Physics.Raycast(ray, 100.0f, LayerMask.GetMask(Constants.GroundRaycastLayerName)))
                {
                    var go = Instantiate(prefabToSpawn, ray.origin, Quaternion.identity);
                    go.SetActive(false);
                    if (returnMinionComponent) minionComponents[i] = go.GetComponent<HealthComponent>();
                    spawnedObjects.Add(go);
                }
                else
                {
                    Debug.Log("AirSpawner: Ground not found");
                }
            }

            StartCoroutine(EnableObjectsWithDelay(spawnedObjects, delayBetweenSpawns));
            return minionComponents.Where(x => x != null).ToArray();
        }

        private static IEnumerator EnableObjectsWithDelay(IEnumerable<GameObject> enabledList, float timeDelay)
        {
            float t = 0;

            foreach (var go in enabledList)
            {
                while (t < timeDelay)
                {
                    t += WorldStateMachine.Instance.DeltaTime;
                    yield return null;
                }

                t = 0;
                go.SetActive(true);
                go.GetComponent<AirDropInitializer>().StartDropping();
            }
        }
    }
}
     
