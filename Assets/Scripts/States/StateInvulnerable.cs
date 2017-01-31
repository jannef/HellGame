using fi.tamk.hellgame.states;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using tamk.fi.hellgame.character;

namespace fi.tamk.hellgame.states
{
    public class StateInvulnerable : StateRunning
    {
        private Renderer _playerRenderer;
        private float _timeUntilNextBlink = .2f;

        public override InputStates StateID
        {
            get { return InputStates.Invulnerable; }
        }

        public override TransitionType CheckTransitionLegality(InputStates toWhichState)
        {
            switch (toWhichState)
            {
                case InputStates.Invulnerable:
                    return TransitionType.Illegal;
                case InputStates.Dead:
                case InputStates.Running:
                case InputStates.Falling:
                    return TransitionType.LegalOneway;
                default:
                    return TransitionType.LegalTwoway;
            }
        }

        public override void HandleInput(float deltaTime)
        {
            base.HandleInput(deltaTime);
            
            RunningMovement(deltaTime);

            if (_stateTime > ControllerHealth.InvulnerabilityLenght)
            {
                ControlledActor.ToPreviousState();
            } else
            {
                InvulnerabilitylBlink();
            }
        }

        private void InvulnerabilitylBlink()
        {
            var t = _stateTime / ControllerHealth.InvulnerabilityLenght;
            if (!(_stateTime >= _timeUntilNextBlink)) return;

            _timeUntilNextBlink = _stateTime + Mathf.Lerp(0.3f, 0.03f, t);
            _playerRenderer.enabled = !_playerRenderer.enabled;
        }

        public override void OnExitState()
        {
            base.OnExitState();
            _playerRenderer.enabled = true;
        }


        public StateInvulnerable(ActorComponent hero) : base(hero)
        {
            _playerRenderer = hero.gameObject.GetComponent<Renderer>();
        }


        public override bool TakeDamage(int howMuch)
        {
            return true;
        }
    }
}
