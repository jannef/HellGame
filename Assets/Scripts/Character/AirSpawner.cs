using fi.tamk.hellgame.utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.character
{
    public class AirSpawner : IndependentBossGO
    {
        [SerializeField]
        private GameObject prefabToSpawn;
        public float _startDelay = 0;
        public float _spawningPeriod;
        public float _spawnAreaRandomNess = 0;
        public float _delayBetweenIndividualSpawns;
        public int _spawnedAmount = 1;

        private float _timer = 0;

        // Use this for initialization
        void Start()
        {
            _timer = (_spawningPeriod - _startDelay);
        }

        public override void Enable(object sender)
        {
            base.Enable(sender);
            Start();
        }

        // Update is called once per frame
        void Update()
        {
            _timer += Time.deltaTime;

            if (_timer > _spawningPeriod)
            {
                _timer = 0;
                SpawnObjects();
            }
        }

        void SpawnObjects()
        {
            StartCoroutine(SpawningInstance());
        }

        IEnumerator SpawningInstance()
        {
            float t = 0;

            for (int i = 0; i < _spawnedAmount; i++)
            {
                while (t < _delayBetweenIndividualSpawns)
                {
                    t += Time.deltaTime;
                    yield return null;
                }

                t = 0;

                var ray = new Ray(transform.position + Random.insideUnitSphere * _spawnAreaRandomNess, Vector3.down);

                if (Physics.Raycast(ray, 100.0f, LayerMask.GetMask(new string[] { Constants.GroundRaycastLayerName })))
                {
                    Debug.Log("AirSpawner: No object to Spawn");
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
