using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using UnityEngine;
using UnityEngine.AI;
using fi.tamk.hellgame.utils;

namespace fi.tamk.hellgame.states
{
    class ShootingSkirmisher : StateAbstract
    {
        private enum SkirmishMode
        {
            FollowingPlayer, Retreating, Stopped
        }

        private Vector3 _retreatPoint;
        private NavMeshAgent _agent;
        private SkirmishMode _currentMode;
        private Transform _targetTransform;
        private float _currentDistanceToPlayer;

        private float _retreatingDistance;
        private float _retreatPointRadius;
        private float _stoppingDistance;

        public ShootingSkirmisher(ActorComponent controlledHero) : base(controlledHero)
        {
            _agent = ControlledActor.gameObject.GetComponent<NavMeshAgent>();
            _retreatingDistance = ControlledActor.ActorNumericData.ActorFloatData[0];
            _retreatPointRadius = ControlledActor.ActorNumericData.ActorFloatData[1];
            _stoppingDistance = ControlledActor.ActorNumericData.ActorFloatData[2];
        }

        public override void OnEnterState()
        {
            base.OnEnterState();
            _retreatPoint = ControlledActor.transform.position;
            _agent.enabled = true;
            _currentMode = SkirmishMode.FollowingPlayer;
        }

        public override void HandleInput(float deltaTime)
        {
            base.HandleInput(deltaTime);

            _targetTransform = ServiceLocator.Instance.GetNearestPlayer(ControlledActor.transform.position);
            if (_targetTransform == null) return;
            if (_targetTransform != null)
            {
                _currentDistanceToPlayer = (ControlledActor.transform.position - _targetTransform.position).magnitude;
            }

            

            switch (_currentMode)
            {
                case SkirmishMode.FollowingPlayer:
                    FollowPlayer();
                    break;
                case SkirmishMode.Retreating:
                    Retreat();
                    break;
                case SkirmishMode.Stopped:
                    Stopped(deltaTime);
                    break;
            }

            if (_targetTransform != null)
            {
                ControlledActor.transform.forward = Vector3.RotateTowards(ControlledActor.transform.forward,
                    new Vector3(_targetTransform.position.x, ControlledActor.transform.position.y,
                    _targetTransform.position.z) - ControlledActor.transform.position,
                    ControlledActor.ActorNumericData.ActorFloatData[2] * deltaTime, 0.0f);
            }

            //ControlledActor.FireGunByIndex(0);
        }

        private void FollowPlayer()
        {
            if (_currentDistanceToPlayer <= _stoppingDistance)
            {
                if (_currentDistanceToPlayer <= _retreatingDistance)
                {
                    _agent.SetDestination(_retreatPoint);
                    _currentMode = SkirmishMode.Retreating;
                    return;
                }

                _agent.enabled = false;
                _currentMode = SkirmishMode.Stopped;
                return;
            }

            _agent.destination = _targetTransform.transform.position;
        }

        private void Retreat()
        {
            if (_currentDistanceToPlayer >= _retreatingDistance)
            {

                _agent.enabled = true;
                _agent.SetDestination(_targetTransform.position);
                _currentMode = SkirmishMode.FollowingPlayer;
                
            }

            if ((ControlledActor.transform.position - _retreatPoint).magnitude <= _retreatPointRadius)
            {
                _agent.enabled = false;
                _currentMode = SkirmishMode.Stopped;
                return;
            }
        }

        private void Stopped(float deltaTime)
        {

            if (_currentDistanceToPlayer <= _retreatingDistance + .5f && (ControlledActor.transform.position - _retreatPoint).magnitude > _retreatPointRadius)
            {
                _agent.enabled = true;
                _agent.SetDestination(_retreatPoint);
                _currentMode = SkirmishMode.Retreating;
                return;
            }

            if (_currentDistanceToPlayer > _stoppingDistance + .5f)
            {
                _agent.enabled = true;
                _agent.SetDestination(_targetTransform.position);
                _currentMode = SkirmishMode.FollowingPlayer;
            }
        }

        public override InputStates StateId
        {
            get
            {
                return InputStates.ShootingSkirmisher;
            }
        }
    }
}
