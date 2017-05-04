using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.world
{

    public class CogTrap : MonoBehaviour
    {
        Vector3 _startingPosition;
        [SerializeField] private Transform _endPosition;
        [SerializeField] private AnimationCurve _movementEasing;
        private bool isInStartingPosition = true;

        public void Awake()
        {
            _startingPosition = transform.position;
            StartMove(25f);
        }

        public void StartMove(float lenght)
        {
            if (isInStartingPosition)
            {
                StartCoroutine(MovingCoroutine(lenght, _movementEasing, _startingPosition, _endPosition.position, transform));
            } else
            {
                StartCoroutine(MovingCoroutine(lenght, _movementEasing, _endPosition.position, _startingPosition, transform));
            }
        }

        private IEnumerator MovingCoroutine(float lenght, AnimationCurve movementEasing, Vector3 startPosition, Vector3 endPosition, Transform transformToMove)
        {
            var t = 0f;

            while (t <= lenght)
            {
                t += WorldStateMachine.Instance.DeltaTime;
                transformToMove.position = Vector3.Lerp(startPosition, endPosition, movementEasing.Evaluate(t / lenght));
                yield return null;
            }
            transformToMove.position = endPosition;

        }
    }
}
