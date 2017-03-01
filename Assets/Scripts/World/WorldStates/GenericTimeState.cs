using fi.tamk.hellgame.world;
using tamk.fi.hellgame.world.states;
using UnityEngine;

namespace fi.tamk.hellgame.world.states
{
    public class GenericTimeState : AbstractWorldState
    {
        protected float Duration;
        protected float InternalTimeScale;

        public GenericTimeState(float duration, float internalTimeScale, WorldStateMachine stateMachine)
            : base(stateMachine)
        {
            Duration = duration;
            InternalTimeScale = internalTimeScale;
        }

        public override float TimeScale
        {
            get { return InternalTimeScale; }
        }

        public override void Timestep(float deltaTime)
        {
            base.Timestep(deltaTime);
            if (StateTime > Duration) StateMachine.EndState();     
        }
    }
}
