using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.world;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace fi.tamk.hellgame.states
{
    class PartollingEnemy : StateAbstract
    {
        private NavMeshAgent _agent;
        public PatrolWayPoint PossibleDestinations;
        private int _destinationWayPointIndex = 0;
        private int _direction = -1;


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
            float maxLenght = 100;
            var i = 0;

            foreach (var destination in PossibleDestinations.WayPointList)
            {
                var lenght = (ControlledActor.transform.position - destination.position).magnitude;

                if (lenght < maxLenght)
                {
                    maxLenght = lenght;
                    _destinationWayPointIndex = i;
                }
                i++;
            }

            if (Random.value >= 0.5)
            {
                _direction = 1;
            }

            NextPoint();
        }

        private void NextPoint()
        {

            if (_destinationWayPointIndex < 0) _destinationWayPointIndex = PossibleDestinations.WayPointList.Length - 1;

            _destinationWayPointIndex = (_destinationWayPointIndex) % (PossibleDestinations.WayPointList.Length);
            _agent.destination = (PossibleDestinations.WayPointList[_destinationWayPointIndex].position);
            _destinationWayPointIndex += _direction;
        }

        public override void HandleInput(float deltaTime)
        {
            base.HandleInput(deltaTime);
            
            if (_agent.remainingDistance < 2f)
            {
                NextPoint();
            }

            ControlledActor.FireGunByIndex(0);

        }
    }
}
