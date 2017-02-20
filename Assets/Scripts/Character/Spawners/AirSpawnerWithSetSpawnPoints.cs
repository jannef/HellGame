using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.utils;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;
using fi.tamk.hellgame.dataholders;

namespace fi.tamk.hellgame.character
{

    public class AirSpawnerWithSetSpawnPoints : MonoBehaviour, ISpawner
    {
        [SerializeField] protected Transform spawnPointsParent;
        [SerializeField] protected Vector3 AirDropOffset;
        private Transform[] availableSpawnPoints;

        void Awake()
        {
            var transformArray = spawnPointsParent.GetComponentsInChildren<Transform>();
            availableSpawnPoints = new Transform[transformArray.Length - 1];

            int availableSpawnPointIndex = 0;

            // This is used to remove the parent from available spawnPoints.
            for (int i = 0; i < transformArray.Length; i++)
            {
                if (transformArray[i].GetInstanceID() != spawnPointsParent.GetInstanceID())
                {
                    availableSpawnPoints[availableSpawnPointIndex] = transformArray[i];
                    availableSpawnPointIndex++;
                }
            }
        }

        public MinionComponent[] Spawn(SpawnerInstruction instructions)
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

            bool returnMinionComponents;

            if (instructions.prefabToSpawn.GetComponent<MinionComponent>() == null)
            {
                returnMinionComponents = false;
            } else
            {
                returnMinionComponents = true;
            }

            return SpawnObjects(instructions.prefabToSpawn, instructions.numberOfSpawns, instructions.possibleSpawnPoints,
                    instructions.delayBetweenSpawns, instructions.spawnAreaRandomness, returnMinionComponents, instructions.SpawnPointSpread);
        }

        private MinionComponent[] SpawnObjects(GameObject prefabToSpawn, int numberToSpawn, int[] spawnPoints, float delayBetweenSpawns, float spawnAreaSize, 
            bool ReturnMinionComponent, SpawnPointSpread spreadType)
        {
            MinionComponent[] minionComponents = new MinionComponent[numberToSpawn];
            List<GameObject> spawnedObjects = new List<GameObject>();
            int spawnPointIndex = 0;
            var maxIndexAmount = spawnPoints.Length;

            if (spreadType == SpawnPointSpread.RandomEvenly)
            {
                System.Random rnd = new System.Random();
                int[] myArray;
                myArray = spawnPoints;
                spawnPoints = myArray.OrderBy(x => rnd.Next()).ToArray();
            }

            for (int i = 0; i < numberToSpawn; i++)
            {
                Ray ray;

                Vector3 targetSpawnPoint;

                if (spawnPoints == null || spawnPoints.Length == 0)
                {
                    if (spreadType == SpawnPointSpread.CompletelyRandom)
                    {
                        spawnPointIndex = UnityEngine.Random.Range(0, maxIndexAmount - 1);
                    }

                    if (spawnPointIndex >= spawnPoints.Length)
                    {
                        spawnPointIndex = 0;
                    }

                    maxIndexAmount = spawnPoints.Length;

                    targetSpawnPoint = availableSpawnPoints[Mathf.Clamp(spawnPointIndex, 0, availableSpawnPoints.Length - 1)].position;

                } else
                {
                    if (spreadType == SpawnPointSpread.CompletelyRandom)
                    {
                        spawnPointIndex = UnityEngine.Random.Range(0, maxIndexAmount - 1);
                    }

                    if (spawnPointIndex >= spawnPoints.Length)
                    {
                        spawnPointIndex = 0;
                    }

                    targetSpawnPoint = availableSpawnPoints[Mathf.Clamp(spawnPoints[spawnPointIndex], 0, availableSpawnPoints.Length - 1)].position;
                }

                

                ray = new Ray(AirDropOffset + targetSpawnPoint + UnityEngine.Random.insideUnitSphere * spawnAreaSize, Vector3.down);

                spawnPointIndex++;


                if (Physics.Raycast(ray, 100.0f, LayerMask.GetMask(new string[] { Constants.GroundRaycastLayerName })))
                {
                    if (prefabToSpawn != null)
                    {
                        GameObject go = Instantiate(prefabToSpawn, ray.origin, Quaternion.identity);
                        go.SetActive(false);
                        if (ReturnMinionComponent) minionComponents[i] = go.GetComponent<MinionComponent>();
                        spawnedObjects.Add(go);
                    }
                    else
                    {
                        Debug.Log("AirSpawner: No object to Spawn");
                    }

                }
                else
                {
                    Debug.Log("AirSpawner: Ground not found");
                }
            }

            StartCoroutine(EnableObjectsWithDelay(spawnedObjects, delayBetweenSpawns));

            return minionComponents.Where(x => x != null).ToArray();
        }

        private IEnumerator EnableObjectsWithDelay(List<GameObject> enabledList, float timeDelay)
        {
            float t = 0;

            foreach (GameObject go in enabledList)
            {

                while (t < timeDelay)
                {
                    t += Time.deltaTime;
                    yield return null;
                }

                t = 0;
                go.SetActive(true);
                go.GetComponent<AirDropInitializer>().StartDropping();
            }
        }
    }
}
     
