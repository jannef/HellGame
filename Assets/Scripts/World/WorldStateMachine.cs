using System.Collections.Generic;
using System.Timers;
using fi.tamk.hellgame.interfaces;
using UnityEngine;
using fi.tamk.hellgame.utils;
using fi.tamk.hellgame.world.states;

namespace fi.tamk.hellgame.world
{
    public class WorldStateMachine : Singleton<WorldStateMachine>
    {
        protected Stack<IWorldState> States = new Stack<IWorldState>();

        public float DeltaTime
        {
            get { return CurrentState == null ? Time.deltaTime : CurrentState.TimeScale * Time.deltaTime; }
        }

        protected IWorldState CurrentState
        {
            get
            {
                return States.Count > 0 ? States.Peek() : null;
            }
        }

        public void EndState()
        {
            if (States.Count >= 2)
            {
                CurrentState.OnExit();
                States.Pop();
                CurrentState.OnEnter();
            }
        }

        protected void EnterState(IWorldState toWhich)
        {
            if (toWhich == null) return;
            if (States.Count > 0) CurrentState.OnExit();
            toWhich.OnEnter();
            States.Push(toWhich);
        }

        public void Start()
        {
            States.Push(new GenericTimeState(float.PositiveInfinity, 1f, this));
            CurrentState.OnEnter();
        }

        public void LateUpdate()
        {
            if (States.Count > 0) CurrentState.Timestep(Time.unscaledDeltaTime);
        }

        public void FreezeFrame()
        {
            EnterState(new GenericTimeState(0.025f, 0.0f, this));
        }

        public void SlowDownPeriod(float lenght, float timescale)
        {
            EnterState(new GenericTimeState(lenght, timescale, this));
        }

        private void PauseTime()
        {
            EnterState(new GenericTimeState(Mathf.Infinity, 0, this));
        }

        private void ResumeTime()
        {
            EndState();
        }

        protected override void Awake()
        {
            base.Awake();
            RoomIdentifier.AddOnPauseListenerAtAwake(PauseTime);
            RoomIdentifier.AddOnResumeListenerAtAwake(ResumeTime);
        }
    }
}
