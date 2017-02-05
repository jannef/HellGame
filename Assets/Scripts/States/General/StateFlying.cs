using fi.tamk.hellgame.states;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using System;

namespace fi.tamk.hellgame
{
    public class StateFlying : StateAbstract
    {
        private AnimationCurve _deccelerationCurve;
        private float _startingSpeed;
        private float _lenght;
        private Vector3 _gravity;
        private Vector3 _direction;
        private IInputState _nextState;

        private float _currentSpeed;

        public StateFlying(ActorComponent controlledHero, IInputState nextState, AnimationCurve deccelerationCurve, float speed, float lenght, 
            Vector3 direction, float gravityStrenght = 1) : base(controlledHero)
        {
            _deccelerationCurve = deccelerationCurve;
            _startingSpeed = speed;
            _lenght = lenght;
            _gravity = new Vector3(0, gravityStrenght, 0);
            _direction = direction;
            _nextState = nextState;
        }

        public override InputStates StateId
        {
            get
            {
                return InputStates.Flying;
            }
        }

        public override void HandleInput(float deltaTime)
        {
            base.HandleInput(deltaTime);

            _currentSpeed = Mathf.Lerp(_startingSpeed, 0, _deccelerationCurve.Evaluate(StateTimer / _lenght));
            HeroAvatar.Move((_direction * _currentSpeed - _gravity) * deltaTime);

            if (HeroAvatar.isGrounded)
            {
                ControlledActor.GoToState(_nextState);
            }
        }
    }
}
