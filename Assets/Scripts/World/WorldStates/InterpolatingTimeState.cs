using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.world;
using UnityEngine;

namespace tamk.fi.hellgame.world.states
{
    public class InterpolatingTimeState : AbstractWorldState
    {
        protected float Duration;
        protected float InternalTimeScale;
        protected float InterpolationLenght;

        public InterpolatingTimeState(float duration, float internalTimeScale, WorldStateMachine stateMachine, float interpolationLenght)
            : base(stateMachine)
        {
            Duration = duration;
            InternalTimeScale = internalTimeScale;
            InterpolationLenght = interpolationLenght;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Time.timeScale = 1;
        }

        public override void OnExit()
        {
            base.OnExit();
            Time.timeScale = 1f;
        }

        public override float TimeScale
        {
            get { return InternalTimeScale; }
        }

        public override void Timestep(float deltaTime)
        {
            base.Timestep(deltaTime);

            if (StateTime <= InterpolationLenght)
            {
                Time.timeScale = Mathf.Lerp(1, InternalTimeScale, StateTime / InterpolationLenght);
            }

            if (StateTime > Duration)
            {
                StateMachine.EndState();
            }
        }
    }
}
