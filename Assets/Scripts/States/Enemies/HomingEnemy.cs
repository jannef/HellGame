using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.utils;
using UnityEngine;

namespace fi.tamk.hellgame.states
{
    public class HomingEnemyState : StateAbstract
    {
        private Transform _targetTransform;
        private readonly Transform _ownTransform;
        private float _retrytimer = 0f;
        private const float RetryTimeout = 1f;

        private HomingEnemyStats _homingStats;
        private float currentSpeed;

        public override InputStates StateId
        {
            get { return InputStates.HomingEnemy; }
        }

        public override void OnEnterState()
        {
            _targetTransform = ServiceLocator.Instance.GetNearestPlayer(ControlledActor.transform.position);
            currentSpeed = 0;
        }

        public override void HandleInput(float deltaTime)
        {
            base.HandleInput(deltaTime);

            if (_targetTransform != null)
            {
                float angleToTarget = Vector3.Angle(_ownTransform.forward, _targetTransform.position - _ownTransform.position);

                if (angleToTarget <= _homingStats.AccelerationAngle)
                {
                   currentSpeed = Mathf.Clamp(currentSpeed + (_homingStats.AccelerationPerSecond * Time.deltaTime), 0, ControlledActor.Speed);
                } else
                {
                    // TODO: Rotating speed is now determined by dashingSpeed
                    currentSpeed = Mathf.Clamp(currentSpeed - (_homingStats.DecelerationPerSecond * Time.deltaTime), 0, ControlledActor.Speed);
                }

                _ownTransform.forward = Vector3.RotateTowards(_ownTransform.forward, _targetTransform.position - _ownTransform.position,
                   Mathf.Lerp(_homingStats.MaxTurningSpeed, _homingStats.MinimumTurningSpeed,
                   _homingStats.TurningSpeedEasing.Evaluate(1 - (ControlledActor.Speed - currentSpeed) / ControlledActor.Speed))
                   * Time.deltaTime, 0.0f);

                HeroAvatar.Move(_ownTransform.forward * currentSpeed * Time.deltaTime);
            }

            _retrytimer += deltaTime;
            if (_retrytimer > RetryTimeout)
            {
                _retrytimer = 0f;
                _targetTransform = ServiceLocator.Instance.GetNearestPlayer(ControlledActor.transform.position);
            }
        }

        public HomingEnemyState(ActorComponent controlledHero) : base(controlledHero)
        {
            _ownTransform = ControlledActor.transform;
            // TODO: Decide what is the best way to add state specific data to actors
            _homingStats = controlledHero.gameObject.GetOrAddComponent<HomingEnemyStats>();
        }
    }
}
