using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.world;
using UnityEngine;

namespace tamk.fi.hellgame.world.states
{
    public class CurvedTimeState : AbstractWorldState
    {
        protected float Duration;
        protected float InternalTimeScale;
        protected float DestinationTimeScale;
        protected float StartTimeScale;
        protected AnimationCurve TimeShiftCurve;

        public CurvedTimeState(float duration, float internalTimeScale, float startTimeScale,
            WorldStateMachine stateMachine, AnimationCurve timeShiftSmoothing)
            : base(stateMachine)
        {
            Duration = duration;
            InternalTimeScale = startTimeScale;
            DestinationTimeScale = internalTimeScale;
            StartTimeScale = startTimeScale;
            TimeShiftCurve = timeShiftSmoothing;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Time.timeScale = StartTimeScale;
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override float TimeScale
        {
            get { return InternalTimeScale; }
        }

        public override void Timestep(float deltaTime)
        {
            base.Timestep(deltaTime);

            if (StateTime <= Duration)
            {
                InternalTimeScale = Mathf.Lerp(StartTimeScale, DestinationTimeScale, TimeShiftCurve.Evaluate(StateTime / Duration));
                Time.timeScale = InternalTimeScale;
            } else
            {
                InternalTimeScale = Mathf.Lerp(StartTimeScale, DestinationTimeScale, TimeShiftCurve.Evaluate(1));
                Time.timeScale = InternalTimeScale;
                StateMachine.EndState();
            }
        }
    }
}
