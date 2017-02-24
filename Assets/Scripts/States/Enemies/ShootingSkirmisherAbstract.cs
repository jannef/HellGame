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
        }

        public override void HandleInput(float deltaTime)
        {
            base.HandleInput(deltaTime);
            if (TargetTransform != null)
            {
                ControlledActor.transform.forward = Vector3.RotateTowards(ControlledActor.transform.forward,
                    new Vector3(TargetTransform.position.x, ControlledActor.transform.position.y,
                    TargetTransform.position.z) - ControlledActor.transform.position,
                    10 * deltaTime, 0.0f);
            }

            ControlledActor.FireGunByIndex(0);
        }
    }
}
