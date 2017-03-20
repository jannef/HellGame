﻿using fi.tamk.hellgame.interfaces;
using System.Collections.Generic;
using fi.tamk.hellgame.dataholders;
using fi.tamk.hellgame.input;
using fi.tamk.hellgame.world;
using UnityEngine;

namespace fi.tamk.hellgame.character
{
    public delegate void TriggerEventDelegate(Collider other);

    public class ActorComponent : MonoBehaviour
    {
        public ActorData ActorNumericData;
        public GameObject HeroObject { get { return gameObject; } }
        public Buttons.ButtonScheme InputBuffer = Buttons.ButtonScheme.None;

        public event TriggerEventDelegate OnTriggerEnterActions;
        public event TriggerEventDelegate OnTriggerStayActions;
        public event TriggerEventDelegate OnTriggerExitActions;

        [HideInInspector] public CharacterController CharacterController;
        protected BulletEmitter[] Emitters;

        private readonly Stack<IInputState> _inputState = new Stack<IInputState>();
        public IInputState CurrentState
        {
            get
            {
                return _inputState.Count > 0 ? _inputState.Peek() : null;
            }
        }

        public bool ToPreviousState()
        {
            if (_inputState.Count < 2) return false;

            CurrentState.OnExitState();
            _inputState.Pop();
            CurrentState.OnResumeState();
            InputBuffer = Buttons.ButtonScheme.None;
            return true;
        }

        public bool GoToState(IInputState toWhichState)
        {
            var transitionType = CurrentState.CheckTransitionLegality(toWhichState.StateId);

            if (_inputState.Count > 0)
            {
                switch (transitionType)
                {
                    case TransitionType.LegalOneway:
                        CurrentState.OnExitState();
                        _inputState.Clear();
                        break;
                    case TransitionType.Illegal:
                        throw new System.Exception(
                            string.Format("Illegal transition passed to CharacterController.GoToState: {0}",
                                toWhichState.ToString()));
                    case TransitionType.LegalTwoway:
                    default:
                        CurrentState.OnSuspendState();
                        break;
                }
            }

            _inputState.Push(toWhichState);
            CurrentState.OnEnterState();
            InputBuffer = Buttons.ButtonScheme.None;

            return true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (OnTriggerEnterActions != null) OnTriggerEnterActions.Invoke(other);
        }

        private void OnTriggerStay(Collider other)
        {
            if (OnTriggerStayActions != null) OnTriggerStayActions.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            if (OnTriggerExitActions != null) OnTriggerExitActions.Invoke(other);
        }

        private void Update()
        {
            if (CurrentState != null) CurrentState.HandleInput(WorldStateMachine.Instance.DeltaTime);
        }

        public void FireGuns()
        {
            foreach (var emitter in Emitters)
            {
                emitter.Fire();
            }
        }

        public void FireGunByIndex(int index)
        {
            if (Emitters.Length > index)
            {
                Emitters[index].Fire();
            }
        }

        protected virtual void Awake()
        {
            // Create own copy of the scriptable object, so that the actors can use it to store
            // instance specific data, without affectign it's peers.
            if (ActorNumericData != null) ActorNumericData = Instantiate(ActorNumericData);
            CharacterController = gameObject.GetOrAddComponent<CharacterController>();
            Emitters = GetComponentsInChildren<BulletEmitter>();
        }

        public void InitializeStateMachine(IInputState initialState)
        {
            if (_inputState.Count == 0) _inputState.Push(initialState);
            CurrentState.OnEnterState();
        }

        public TakeDamageDelegate TakeDamage
        {
            get
            {
                if (CurrentState != null) return CurrentState.TakeDamage;
                return null;
            } 
        }

        public virtual bool RequestStateChange(InputStates whichState)
        {
            return CurrentState != null && CurrentState.RequestStateChange(whichState);
        }
    }   
}
