using fi.tamk.hellgame.utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.character
{

    public class AirSpawnerWithSetSpawnPoints : AirSpawner
    {
        [SerializeField] protected Transform[] spawnPoints;

        public void SpawnObjects(int number, bool randomPosition = false)
        {
            if (randomPosition)
            {
                RandomisedSpawningInstance(number);
            } else
            {
                SpawningInstance(number);
            }
        }

        public void SpawnObjects(int number, Vector3 position)
        {
            SpawningInstance(number, position);
        }

        public void SpawnObjectsToRandomSpawnPoint(int number)
        {
            SpawningInstance(number, spawnPoints[Random.Range(0, spawnPoints.Length)].position);
        }

        protected IEnumerator SpawningInstance(int number, Vector3 position)
        {
            var t = 0f;
            int spawnPointIndex = 0;

            for (var i = 0; i < number; i++)
            {
                while (t < DelayBetweenIndividualSpawns)
                {
                    t += Time.deltaTime;
                    yield return null;
                }

                t = 0;

                if (spawnPointIndex >= spawnPoints.Length) spawnPointIndex = 0;

                var ray = new Ray(AirDropOffset + position + Random.insideUnitSphere * SpawnAreaSize, Vector3.down);
                spawnPointIndex++;


                if (Physics.Raycast(ray, 100.0f, LayerMask.GetMask(new string[] { Constants.GroundRaycastLayerName })))
                {
                    if (prefabToSpawn != null)
                    {
                        Instantiate(prefabToSpawn, ray.origin, Quaternion.identity);
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

                yield return null;
            }
        }

        protected IEnumerator RandomisedSpawningInstance(int number)
        {
            var t = 0f;

            for (var i = 0; i < number; i++)
            {
                while (t < DelayBetweenIndividualSpawns)
                {
                    t += Time.deltaTime;
                    yield return null;
                }

                t = 0;

                var ray = new Ray(AirDropOffset + spawnPoints[Random.Range(0, spawnPoints.Length)].position + Random.insideUnitSphere * SpawnAreaSize, Vector3.down);


                if (Physics.Raycast(ray, 100.0f, LayerMask.GetMask(new string[] { Constants.GroundRaycastLayerName })))
                {
                    if (prefabToSpawn != null)
                    {
                        Instantiate(prefabToSpawn, ray.origin, Quaternion.identity);
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

                yield return null;
            }
        }

        protected IEnumerator SpawningInstance(int number)
        {
            var t = 0f;
            int spawnPointIndex = 0;

            for (var i = 0; i < number; i++)
            {
                while (t < DelayBetweenIndividualSpawns)
                {
                    t += Time.deltaTime;
                    yield return null;
                }

                t = 0;

                if (spawnPointIndex >= spawnPoints.Length) spawnPointIndex = 0;

                var ray = new Ray(AirDropOffset + spawnPoints[spawnPointIndex].position + Random.insideUnitSphere * SpawnAreaSize, Vector3.down);
                spawnPointIndex++;


                if (Physics.Raycast(ray, 100.0f, LayerMask.GetMask(new string[] { Constants.GroundRaycastLayerName })))
                {
                    if (prefabToSpawn != null)
                    {
                        Instantiate(prefabToSpawn, ray.origin, Quaternion.identity);
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

                yield return null;
            }
        }
    }
}
