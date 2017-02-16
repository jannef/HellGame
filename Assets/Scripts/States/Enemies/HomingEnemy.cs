using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.utils;
using UnityEngine;
using UnityEngine.AI;

namespace fi.tamk.hellgame.states
{
    public class HomingEnemyState : StateAbstract
    {
        private NavMeshAgent _agent;

        public override InputStates StateId
        {
            get { return InputStates.HomingEnemy; }
        }

        public override void OnEnterState()
        {
            _agent.enabled = true;
        }

        public override void HandleInput(float deltaTime)
        {
            base.HandleInput(deltaTime);

            var agent = ServiceLocator.Instance.GetNearestPlayer(Vector3.zero);
            if (agent != null) _agent.destination = agent.transform.position;
        }

        public HomingEnemyState(ActorComponent controlledHero) : base(controlledHero)
        {
            _agent = ControlledActor.gameObject.GetComponent<NavMeshAgent>();
        }
    }
}
