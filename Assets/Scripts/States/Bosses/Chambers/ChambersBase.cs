using UnityEngine;
using System.Collections;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.utils;
using UnityEngine.AI;

namespace fi.tamk.hellgame.states
{
    public abstract class ChambersBase : StateAbstract
    {
        protected NavMeshAgent NavigationAgent;
        protected Transform PlayerTransform;
        protected HealthComponent Health;

        protected ChambersBase(ActorComponent controlledHero, ChambersBase clonedState = null)
            : base(controlledHero)
        {
            if (clonedState == null)
            {
                NavigationAgent = ControlledActor.GetComponent<NavMeshAgent>();
                PlayerTransform = ServiceLocator.Instance.GetNearestPlayer(Vector3.zero);
            }
            else
            {
                NavigationAgent = clonedState.NavigationAgent;
                PlayerTransform = clonedState.PlayerTransform;
            }
        }

        public override TransitionType CheckTransitionLegality(InputStates toWhichState)
        {
            return TransitionType.LegalOneway;
        }

        public override void OnEnterState()
        {
            Health.HealthChangeEvent += OnHealthChange;
        }

        protected virtual void OnHealthChange(float percentage, int currentHp, int maxHp)
        {

        }

        public override void OnExitState()
        {
            Health.HealthChangeEvent -= OnHealthChange;
        }
    }
}
