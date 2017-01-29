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
        private bool _isTransparent;
        private float _timeUntilNextBlink = .2f;

        public override InputStates StateID
        {
            get { return InputStates.Invulnerable; }
        }

        public override TransitionType CheckTransitionLegality(InputStates toWhichState)
        {
            switch (toWhichState)
            {
                case InputStates.Dashing:
                case InputStates.Paused:
                    return TransitionType.LegalTwoway;
                case InputStates.Dead:
                case InputStates.Running:
                    return TransitionType.LegalOneway;
                default:
                    return TransitionType.Illegal;
            }
        }

        public override void HandleInput(float deltaTime)
        {
            _stateTime += deltaTime;

            var movementDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            var rawMousePosition = MouseLookUp.Instance.GetMousePosition();
            HeroAvatar.transform.LookAt(new Vector3(rawMousePosition.x, HeroAvatar.transform.position.y, rawMousePosition.z));
            HeroAvatar.Move(movementDirection * HeroStats.Speed * deltaTime);

            if (Input.GetButtonDown("Jump"))
            {
                ControlledCharacter.GoToState(new StateDashing(ControlledCharacter, movementDirection.normalized));
            }
            else if (Input.GetButton("Fire2"))
            {
                ControlledCharacter.FireGunByIndex(1);
            }
            if (Input.GetButton("Fire1"))
            {
                ControlledCharacter.FireGunByIndex(0);
            }

            if (_stateTime > HeroStats.InvulnerabilityLenght)
            {
                ControlledCharacter.ToPreviousState();
            } else
            {
                InvulnerabilitylBlink();
            }
        }

        private void InvulnerabilitylBlink()
        {
            float t = _stateTime / HeroStats.InvulnerabilityLenght;

            if (_stateTime >= _timeUntilNextBlink)
            {
                _timeUntilNextBlink = _stateTime + Mathf.Lerp(0.3f, 0.03f, t);
                _playerRenderer.enabled = !_playerRenderer.enabled;
            }
        }

        public override void OnExitState()
        {
            base.OnExitState();
            _playerRenderer.enabled = true;
        }


        public StateInvulnerable(HeroController hero) : base(hero)
        {
            _playerRenderer = ControlledCharacter.gameObject.GetComponent<Renderer>();
            
        }


        public override void TakeDamage(int howMuch)
        {

        }
    }
}
