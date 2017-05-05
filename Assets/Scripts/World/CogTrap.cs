using fi.tamk.hellgame.utils;
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
        [SerializeField] private float singleTripLenght;
        private bool isInStartingPosition = true;

        public void Awake()
        {
            _startingPosition = transform.position;
            StartCoroutine(StaticCoroutines.DoAfterDelay(0.2f, StartMove));
        }

        public void StopTrap()
        {

        }

        public void StartMove()
        {
            if (isInStartingPosition)
            {
                StartCoroutine(MovingCoroutine(singleTripLenght, _movementEasing, _startingPosition, _endPosition.position, transform));
            } else
            {
                StartCoroutine(MovingCoroutine(singleTripLenght, _movementEasing, _endPosition.position, _startingPosition, transform));
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
            isInStartingPosition = !isInStartingPosition;
            StartMove();
        }
    }
}
