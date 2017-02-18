using fi.tamk.hellgame.world;
using tamk.fi.hellgame.world.states;
using UnityEngine;

namespace fi.tamk.hellgame.world.states
{
    public class GenericTimeState : AbstractWorldState
    {
        protected float Duration;
        protected float Timescale;

        public GenericTimeState(float duration, float timescale, WorldStateMachine stateMachine)
            : base(stateMachine)
        {
            Duration = duration;
            Timescale = timescale;
        }

        public override void OnEnter()
        {
            Time.timeScale = Timescale;
        }

        public override void Timestep(float deltaTime)
        {
            base.Timestep(deltaTime);
            if (StateTime > Duration) StateMachine.EndState();     
        }
    }
}
