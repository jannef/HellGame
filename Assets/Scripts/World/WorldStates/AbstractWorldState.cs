using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.world;
using UnityEngine;

namespace tamk.fi.hellgame.world.states
{
    public abstract class AbstractWorldState : IWorldState
    {
        protected float StateTime = 0f;
        protected WorldStateMachine StateMachine;

        protected AbstractWorldState(WorldStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        public abstract float TimeScale { get; }

        public virtual void Timestep(float deltaTime)
        {
            StateTime += deltaTime;
        }

        public virtual void OnEnter()
        {
            StateTime = 0f;
        }

        public virtual void OnExit()
        {
            
        }
    }
}
