using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace fi.tamk.hellgame.states
{
    public class ShootingSkirmisherStoppedState : ShootingSkirmisherAbstract
    {
        private float _startingSpeed;
        private float _startingAcceleration;
        private Vector3 _startingPosition;
       
        public ShootingSkirmisherStoppedState(ActorComponent ac, ShootingSkirmisherAbstract shootingSkirmisher = null) : base(ac, shootingSkirmisher)
        {
        }

        public override void HandleInput(float deltaTime)
        {
            base.HandleInput(deltaTime);

            if (CurrentDistanceToPlayer < ControlledActor.ActorNumericData.ActorFloatData[0] && 
                (ControlledActor.transform.position - RetreatPoint).magnitude > ControlledActor.ActorNumericData.ActorFloatData[2])
            {
                ControlledActor.GoToState(new ShootingSkirmisherRetreatState(ControlledActor, this));
                return;
            }

            if (CurrentDistanceToPlayer > ControlledActor.ActorNumericData.ActorFloatData[1])
            {
                ControlledActor.GoToState(new ShootingSkirmisherFollowingState(ControlledActor, this));
                return;
            }

            if (Agent.remainingDistance < 0.01f)
            {
                var random = Random.insideUnitCircle * 5;
                Agent.destination = _startingPosition + new Vector3(random.x, 0, random.y);
            }
        }

        public override void OnEnterState()
        {
            base.OnEnterState();
            _startingAcceleration = Agent.acceleration;
            _startingPosition = ControlledActor.transform.position;
            _startingSpeed = Agent.speed;
            Agent.acceleration = _startingAcceleration / 5;
            Agent.speed = _startingSpeed / 5;
            Agent.SetDestination(ControlledActor.transform.position);
        }

        public override void OnExitState()
        {
            base.OnExitState();
            Agent.speed = _startingSpeed;
            Agent.acceleration = _startingAcceleration;
        }
    }
}
