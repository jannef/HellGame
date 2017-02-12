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
            _agent.destination = ServiceLocator.Instance.GetNearestPlayer(Vector3.zero).transform.position;
        }

        public HomingEnemyState(ActorComponent controlledHero) : base(controlledHero)
        {
            _agent = ControlledActor.gameObject.GetComponent<NavMeshAgent>();
        }
    }
}
