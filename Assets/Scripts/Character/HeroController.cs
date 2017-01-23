using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.states;
using System.Collections.Generic;
using UnityEngine;
namespace fi.tamk.hellgame.character
{
    public class HeroController : MonoBehaviour
    {
        public GameObject HeroObject { get { return gameObject; } }
        [HideInInspector] public CharacterStats HeroStats;
        [HideInInspector] public CharacterController CharacterController;
        private Stack<IInputState> _inputState = new Stack<IInputState>();
        private IInputState _currentState
        {
            get
            {
                if (_inputState.Count > 0) { return _inputState.Peek(); }
                return null;
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

            if (transitionType == TransitionType.LegalOneway)
            {
                if (_inputState.Count < 1) return false;

                _currentState.OnExitState();
                _inputState.Clear();
            }
            else if (transitionType == TransitionType.Illegal)
            {
                throw new System.Exception(string.Format("Illegal transition passed to CharacterController.GoToState: {0}", toWhichState.ToString()));
            }
            else
            {
                _currentState.OnSuspendState();
            }
            
            _inputState.Push(toWhichState);
            _currentState.OnEnterState();

            return true;
        }

        private void Update()
        {
            _currentState.HandleInput(Time.deltaTime);
        }

        void Awake()
        {
            HeroStats = gameObject.GetOrAddComponent<CharacterStats>();
            CharacterController = gameObject.GetOrAddComponent<CharacterController>();
            _inputState.Push(new StateRunning(this));
        }
    }   
}
