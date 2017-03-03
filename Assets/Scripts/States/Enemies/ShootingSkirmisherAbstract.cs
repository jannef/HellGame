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
    public abstract class ShootingSkirmisherAbstract : StateAbstract
    {
        public Vector3 RetreatPoint;
        public bool RetreatPointSet = false;
        public NavMeshAgent Agent;
        private LayerMask _raycastLayerMask;
        private float _rayCastRetrytimer = 0f;
        public Transform TargetTransform
        {
            get
            {
                return _targetTransform ?? (_targetTransform = ServiceLocator.Instance.GetNearestPlayer(ControlledActor.transform.position));
            }

            set
            {
                _targetTransform = value;
            }
        }
        private Transform _targetTransform;

        protected float CurrentDistanceToPlayer
        {
            get
            {
                 return TargetTransform == null ? -1f : (ControlledActor.transform.position - TargetTransform.position).magnitude;
            }
        }

        public override InputStates StateId
        {
            get
            {
                return InputStates.ShootingSkirmisher;
            }
        }

        public override void OnEnterState()
        {
            base.OnEnterState();

            Agent.enabled = true;

            if (!RetreatPointSet)
            {
                RetreatPointSet = true;
                RetreatPoint = ControlledActor.transform.position;
            }
        }

        public override TransitionType CheckTransitionLegality(InputStates toWhichState)
        {
            return TransitionType.LegalOneway;
        }

        public ShootingSkirmisherAbstract(ActorComponent controlledHero, ShootingSkirmisherAbstract shootingSkirmisher = null) : base(controlledHero)
        {
            if (shootingSkirmisher == null)
            {
                Agent = ControlledActor.gameObject.transform.GetComponent<NavMeshAgent>();
                return;
            }
            Agent = shootingSkirmisher.Agent;
            RetreatPointSet = shootingSkirmisher.RetreatPointSet;
            RetreatPoint = shootingSkirmisher.RetreatPoint;
            TargetTransform = shootingSkirmisher.TargetTransform;
            _raycastLayerMask = LayerMask.GetMask(Constants.ObstacleLayerName, Constants.PlayerLayerName, Constants.PlayerDashingLayerName);
        }

        public override void HandleInput(float deltaTime)
        {
            base.HandleInput(deltaTime);
            if (TargetTransform != null)
            {
                ControlledActor.transform.forward = Vector3.RotateTowards(ControlledActor.transform.forward,
                    new Vector3(TargetTransform.position.x, ControlledActor.transform.position.y,
                    TargetTransform.position.z) - ControlledActor.transform.position,
                    100 * deltaTime, 0.0f);

                _rayCastRetrytimer += deltaTime;
                if (_rayCastRetrytimer > 0.1f)
                {
                    _rayCastRetrytimer = 0;
                    var ray = new Ray(ControlledActor.transform.position,
                        TargetTransform.position - ControlledActor.transform.position);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 100f, _raycastLayerMask))
                    {
                        if (hit.transform.gameObject.layer == Constants.PlayerLayer)
                        {
                            ControlledActor.FireGunByIndex(0);
                        }
                    }
                }


            }
            
        }
    }
}
