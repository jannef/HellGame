using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.world;
using UnityEngine;
using UnityEngine.AI;

namespace fi.tamk.hellgame.states
{
    class PartollingEnemy : StateAbstract
    {
        private NavMeshAgent _agent;
        public PatrolWayPoint PossibleDestinations;
        private int _destinationWayPointIndex = -1;


        public PartollingEnemy(ActorComponent controlledHero) : base(controlledHero)
        {
            _agent = ControlledActor.gameObject.GetComponent<NavMeshAgent>();
        }

        public override InputStates StateId
        {
            get { return InputStates.PatrollingEnemy; }
        }

        public override void OnEnterState()
        {
            _agent.enabled = true;
            PossibleDestinations = ControlledActor.ActorNumericData.GoData[0].GetComponent<PatrolWayPoint>();

            NextPoint();
        }

        private void NextPoint()
        {
            _destinationWayPointIndex = (_destinationWayPointIndex + 1) % PossibleDestinations.WayPointList.Length;
            _agent.destination = (PossibleDestinations.WayPointList[_destinationWayPointIndex].position);
        }

        public override void HandleInput(float deltaTime)
        {
            base.HandleInput(deltaTime);
            
            if (_agent.remainingDistance < 1f)
            {
                NextPoint();
            }

        }
    }
}
