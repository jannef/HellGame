using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.world
{

    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private float _spawnLenght;
        [SerializeField] private AnimationCurve _movementCurve;

        public void StartSpawning(GameObject player, Vector3 startingPosition)
        {
            player.SetActive(false);
            StartCoroutine(SpawningRoutine(startingPosition, player));
        }

        private IEnumerator SpawningRoutine(Vector3 endPosition, GameObject player)
        {
            var t = 0f;
            Vector3 startingPosition = transform.position;

            while (t >= 1)
            {
                transform.position = Vector3.Lerp(startingPosition, endPosition, _movementCurve.Evaluate(t));
                t += Time.deltaTime;
            }

            player.transform.position = endPosition;
            player.SetActive(true);
            var particleEffects = GetComponentsInChildren<ParticleSystem>();
            if (particleEffects != null)
            {
                foreach (ParticleSystem system in particleEffects)
                {
                    system.Stop();
                }
            }

            yield return null;
        }
    }
}
