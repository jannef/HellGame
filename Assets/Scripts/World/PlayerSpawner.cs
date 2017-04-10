using fi.tamk.hellgame.character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.world
{

    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private float _spawnLenght;
        [SerializeField] private AnimationCurve _movementCurve;
        [SerializeField] private Transform _startPosition;

        public bool StartSpawning(GameObject player)
        {
            var dashComponent = player.GetComponent<PlayerDash>();
            var actorComponent = player.GetComponent<ActorComponent>();

            if (actorComponent == null || dashComponent == null)
            {
                new UnityException("PlayerSpawner; PlayerDash or ActorComponent not found in target GameObject");
                return false;
            }

            StartCoroutine(SpawningRoutine(transform.position, _startPosition.position, actorComponent, dashComponent, _spawnLenght));
            return true;
        }

        private IEnumerator SpawningRoutine(Vector3 endPosition, Vector3 startingPosition, ActorComponent playerActor, PlayerDash dashComponent, float lenght)
        {
            var t = 0f;
            playerActor.enabled = false;
            dashComponent.StartDash();

            while (t <= 1)
            {
                playerActor.transform.position = Vector3.Lerp(startingPosition, endPosition, _movementCurve.Evaluate(t));
                t += Time.deltaTime / lenght;
                yield return null;
            }

            playerActor.enabled = true;
            playerActor.transform.forward = endPosition - startingPosition;
            dashComponent.StopDash();
            playerActor.transform.position = endPosition;
        }
    }
}
