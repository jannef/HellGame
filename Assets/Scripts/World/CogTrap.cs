using fi.tamk.hellgame.utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace fi.tamk.hellgame.world
{

    public class CogTrap : MonoBehaviour
    {
        Vector3 _startingPosition;
        [SerializeField] private Transform _endPosition;
        [SerializeField] private AnimationCurve _movementEasing;
        [SerializeField] private float singleTripLenght;
        [SerializeField] private float telegraphLenght;
        [SerializeField] private float cooldownLenght;
        [SerializeField] private float cooldownRandomness;
        public UnityEvent TelegraphFromStartPoint;
        public UnityEvent TelegraphFromEndPoint;
        public UnityEvent CogLaunchedFromStartEvent;
        public UnityEvent CogLaunchedFromEndEvent;
        private bool isInStartingPosition = true;
        private float endCoolDown;
        private bool isRunning = true;

        public void Start()
        {
            _startingPosition = transform.position;
            StartCoroutine(StaticCoroutines.DoAfterDelay(0.2f, StartAttack));
            RoomIdentifier.RoomCompleted += StopTrap;
        }

        public void StopTrap()
        {
            RoomIdentifier.RoomCompleted -= StopTrap;
            isRunning = false;
        }

        public void StartAttack()
        {
            if (!isRunning)
            {
                StopAllCoroutines();
                return;
            }

            var delay = Random.value * cooldownRandomness;
            endCoolDown = cooldownRandomness - delay;
            StartCoroutine(StaticCoroutines.DoAfterDelay(cooldownLenght + delay, StartTelegraph));
        }

        public void StartTelegraph()
        {
            if (!isRunning)
            {
                StopAllCoroutines();
                return;
            }

            if (isInStartingPosition)
            {
                if (TelegraphFromStartPoint != null) TelegraphFromStartPoint.Invoke();
            } else
            {
                if (TelegraphFromEndPoint != null) TelegraphFromEndPoint.Invoke();
            }
            
            StartCoroutine(StaticCoroutines.DoAfterDelay(telegraphLenght, StartMove));
        }

        public void StartMove()
        {
            if (!isRunning)
            {
                StopAllCoroutines();
                return;
            }

            if (isInStartingPosition)
            {
                if (CogLaunchedFromStartEvent != null) CogLaunchedFromStartEvent.Invoke();
                StartCoroutine(MovingCoroutine(singleTripLenght, _movementEasing, _startingPosition, _endPosition.position, transform));
            } else
            {
                if (CogLaunchedFromEndEvent != null) CogLaunchedFromEndEvent.Invoke();
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
            StartCoroutine(StaticCoroutines.DoAfterDelay(endCoolDown, StartAttack));
        }
    }
}
