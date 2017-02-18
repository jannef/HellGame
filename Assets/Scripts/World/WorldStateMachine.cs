using System.Collections.Generic;
using fi.tamk.hellgame.interfaces;
using UnityEngine;
using fi.tamk.hellgame.utils;
using fi.tamk.hellgame.world.states;

namespace fi.tamk.hellgame.world
{
    public class WorldStateMachine : Singleton<WorldStateMachine>
    {
        protected Stack<IWorldState> States = new Stack<IWorldState>();

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

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.X))
            {          
                EnterState(new GenericTimeState(0.1f, 0f, this));
            }
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
    }
}
