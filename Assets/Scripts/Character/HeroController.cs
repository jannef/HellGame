﻿using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.states;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.character
{
    public class HeroController : MonoBehaviour
    {
        public BulletEmitter[] Emitters;

        public GameObject HeroObject { get { return gameObject; } }
        [HideInInspector] public CharacterStats HeroStats;
        [HideInInspector] public CharacterController CharacterController;
        private Stack<IInputState> _inputState = new Stack<IInputState>();
        private IInputState _currentState
        {
            get
            {
                return _inputState.Count > 0 ? _inputState.Peek() : null;
            }
        }

        public bool ToPreviousState()
        {
            if (_inputState.Count < 2) return false;

            _currentState.OnExitState();
            _inputState.Pop();
            _currentState.OnResumeState();
            return true;
        }

        public bool GoToState(IInputState toWhichState)
        {
            var transitionType = _currentState.CheckTransitionLegality(toWhichState.StateID);

            switch (transitionType)
            {
                case TransitionType.LegalOneway:
                    if (_inputState.Count < 1) return false;

                    _currentState.OnExitState();
                    _inputState.Clear();
                    break;
                case TransitionType.Illegal:
                    throw new System.Exception(string.Format("Illegal transition passed to CharacterController.GoToState: {0}", toWhichState.ToString()));
                default:
                    _currentState.OnSuspendState();
                    break;
            }
            
            _inputState.Push(toWhichState);
            _currentState.OnEnterState();

            return true;
        }

        private void Update()
        {
            _currentState.HandleInput(Time.deltaTime);
        }

        public void Action()
        {
            foreach (var Emitter in Emitters)
            {
                Emitter.Fire();
            }
        }

        private void Awake()
        {
            HeroStats = gameObject.GetOrAddComponent<CharacterStats>();
            CharacterController = gameObject.GetOrAddComponent<CharacterController>();
            _inputState.Push(new StateRunning(this));

            Emitters = GetComponents<BulletEmitter>();
        }
    }   
}
