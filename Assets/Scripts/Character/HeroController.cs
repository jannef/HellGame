using fi.tamk.hellgame.interfaces;
using System.Collections.Generic;
using UnityEngine;
namespace fi.tamk.hellgame.character
{
    public class HeroController : MonoBehaviour
    {
        public GameObject HeroObject { get { return gameObject; } }
        private Stack<IInputState> _inputState = new Stack<IInputState>();
        private IInputState _currentState
        {
            get
            {
                if (_inputState.Count > 0) { return _inputState.Peek(); }
                return null;
            }
        }

        private bool ToPreviousState()
        {
            if (_inputState.Count < 2) return false;

            _currentState.OnExitState();
            _inputState.Pop();
            _currentState.OnResumeState();
            return true;
        }

        private bool GoToState(IInputState toWhichState)
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

        void Awake()
        {
            _inputState.Push(new StateRunning(this));
            Debug.Log(_currentState);
            GoToState(new StatePaused(this));
            Debug.Log(_currentState);
            ToPreviousState();
            Debug.Log(_currentState);
        }
    }   
}
