using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.World.WorldStates;
using fi.tamk.hellgame.interfaces;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

namespace fi.tamk.hellgame.world
{
    public class WorldStateMachine : MonoBehaviour
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

        public void Awake()
        {
            States.Push(new GenericTimeState(float.PositiveInfinity, 1f, this));
            CurrentState.OnEnter();
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                Debug.Log("is pressed");
                EnterState(new GenericTimeState(0.1f, 0f, this));
            }
        }

        public void LateUpdate()
        {
            if (States.Count > 0) CurrentState.Timestep(Time.unscaledDeltaTime);
        }

        public void FreezeFrame()
        {
            
        }
    }
}
