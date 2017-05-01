using fi.tamk.hellgame.dataholders;
using fi.tamk.hellgame.utils;
using System;
using System.Collections;
using fi.tamk.hellgame.character;
using UnityEngine;
using UnityEngine.Events;

namespace fi.tamk.hellgame.world
{

    public class LaserTrap : MonoBehaviour
    {
        public UnityEvent startLaser;
        public UnityEvent stopLaser;
        public UnityEvent deathEvent;
        [SerializeField] private Transform _startingPoint;
        [SerializeField] private Transform _secondPoint;
        [SerializeField] private WallBossMovement _movementStats;
        [SerializeField] private float _cooldown;
        [SerializeField] private LaserEmitter _emitter;

        private bool _isInStartingPosition = true;
        private Action _stopLaserAction;

        public void Stop()
        {
            if (_stopLaserAction != null) _stopLaserAction.Invoke();
            if (deathEvent != null) deathEvent.Invoke();
            StopAllCoroutines();
            
        }

        public void Activate()
        {
            StartCoroutine(StaticCoroutines.DoAfterDelay(_cooldown, StartAttack));
        }

        private void StartAttack()
        {

            if (startLaser != null) startLaser.Invoke();

            _stopLaserAction = _emitter.FireUntilFurtherNotice();

            Vector3 endDestination = _startingPoint.position;

            if (_isInStartingPosition)
            {
                endDestination = _secondPoint.position;
            }

            StartCoroutine(MovementCoroutine(transform.position, endDestination));
        }

        private void StopAttack()
        {
            
            if (_stopLaserAction != null) _stopLaserAction.Invoke();
            if (stopLaser != null) stopLaser.Invoke();

            _isInStartingPosition = !_isInStartingPosition;

            StartCoroutine(StaticCoroutines.DoAfterDelay(_cooldown, StartAttack));
        }

        private IEnumerator MovementCoroutine(Vector3 startingPosition, Vector3 endPosition)
        {
            var t = 0f;

            while (t <= 1)
            {
                t += WorldStateMachine.Instance.DeltaTime / _movementStats.MovementDelay;
                yield return null;
            }

            t = 0f;

            while (t <= 1)
            {
                transform.position = Vector3.LerpUnclamped(startingPosition, endPosition, _movementStats.MovementCurve.Evaluate(t));
                t += WorldStateMachine.Instance.DeltaTime / _movementStats.MovementSpeed;
                yield return null;
            }

            StopAttack();
        }

    }
}
