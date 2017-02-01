using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.states;
using System.Collections.Generic;
using fi.tamk.hellgame.utils.Stairs.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace fi.tamk.hellgame.character
{
    [RequireComponent(typeof(HealthComponent))]
    public class ActorComponent : MonoBehaviour
    {
        public float Speed = 1;
        public float DashSpeed = 10;
        public float DashDuration = 0.75f;
        public GameObject HeroObject { get { return gameObject; } }

        [HideInInspector] public CharacterController CharacterController;

        protected BulletEmitter[] Emitters;

        private readonly Stack<IInputState> _inputState = new Stack<IInputState>();
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
            if (_currentState != null) _currentState.HandleInput(Time.deltaTime);
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

        private void Awake()
        {
            CharacterController = gameObject.GetOrAddComponent<CharacterController>();
            Emitters = GetComponents<BulletEmitter>();
        }

        public void InitializeStateMachine(IInputState initialState)
        {
            if (_inputState.Count == 0) _inputState.Push(initialState);
            _currentState.OnEnterState();
        }

        public TakeDamageDelegate TakeDamage
        {
            get
            {
                if (_currentState != null) return _currentState.TakeDamage;
                return null;
            } 
        }
    }   
}
