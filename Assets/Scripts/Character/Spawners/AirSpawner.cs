using fi.tamk.hellgame.utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.character
{
    public class AirSpawner : MonoBehaviour
    {
        [SerializeField] protected GameObject prefabToSpawn;
        public float SpawnAreaSize = 0;
        public float DelayBetweenIndividualSpawns = 0f;
        public Vector3 AirDropOffset = new Vector3(0f, 54f, 0f);
        public int NumberOfSpawns = 1;

        public void SpawnObjects()
        {
            StartCoroutine(SpawningInstance());
        }

        protected virtual IEnumerator SpawningInstance()
        {
            var t = 0f;

            for (var i = 0; i < NumberOfSpawns; i++)
            {
                while (t < DelayBetweenIndividualSpawns)
                {
                    t += Time.deltaTime;
                    yield return null;
                }

                t = 0;

                var ray = new Ray(AirDropOffset + transform.position + Random.insideUnitSphere * SpawnAreaSize, Vector3.down);

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

                } else
                {
                    Debug.Log("AirSpawner: Ground not found");
                }

                yield return null;
            }
        }
    }
}
