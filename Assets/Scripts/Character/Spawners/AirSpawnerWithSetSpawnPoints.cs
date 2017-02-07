using fi.tamk.hellgame.utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.character
{

    public class AirSpawnerWithSetSpawnPoints : AirSpawner
    {
        [SerializeField] protected Transform[] spawnPoints;

        protected override IEnumerator SpawningInstance()
        {
            var t = 0f;
            int spawnPointIndex = 0;

            for (var i = 0; i < NumberOfSpawns; i++)
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
