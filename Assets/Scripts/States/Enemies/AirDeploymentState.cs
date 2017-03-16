using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace fi.tamk.hellgame.states
{
    class AirDeploymentState : StateAbstract
    {
        public event UnityAction ExitStateSignal;

        public IInputState _startingState;
        private float _fallingDuration = 3f;
        private AnimationCurve _animationCurve;
        private Vector3 _landingPosition;
        private Vector3 _startingPosition;
        private bool _isDeploying = true;

        public AirDeploymentState(ActorComponent controlledHero, IInputState startingState, Vector3 startingPosition, 
            float fallingDuration, AnimationCurve fallingCurve, Vector3 landingCoordinates)
            : base(controlledHero)
        {
            _startingState = startingState;
            _fallingDuration = fallingDuration;
            _animationCurve = fallingCurve;
            _startingPosition = startingPosition;
            _landingPosition = landingCoordinates;
        }

        public override bool TakeDamage(int howMuch, ref int health, ref bool flinch)
        {
            // Prevent dying midair to get rid of many problems.
            return true;
        }

        public override InputStates StateId
        {
            get { return InputStates.AirDeploymentState; }
        }

        public override TransitionType CheckTransitionLegality(InputStates toWhichState)
        {
            switch (toWhichState)
            {
                case InputStates.Paused:
                    return TransitionType.LegalTwoway;
                default:
                    return TransitionType.LegalOneway;
            }
        }

        protected override void CheckForFalling()
        {
            
        }

        public override void OnExitState()
        {
            base.OnExitState();
            if (ExitStateSignal != null) ExitStateSignal.Invoke();
            if (_isDeploying) ControlledActor.transform.position = _landingPosition;
        }

        public override void HandleInput(float deltaTime)
        {
            base.HandleInput(deltaTime);

            var newPosition = Vector3.Lerp(_startingPosition, _landingPosition,
                _animationCurve.Evaluate(StateTime / _fallingDuration));

            ControlledActor.transform.position = newPosition;

            if (StateTime >= _fallingDuration)
            {
                _isDeploying = true;
                ControlledActor.GoToState(_startingState);
            }
        }
    }
}
