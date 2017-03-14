using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using UnityEngine;
using fi.tamk.hellgame.dataholders;

namespace fi.tamk.hellgame.states
{
    class WallBossMove : StateAbstract
    {
        private Vector3 _startingPosition;
        private Vector3 _finalPosition;
        private float _moveLenght;
        private float _t = 0;
        private float _setupTime;
        private AnimationCurve _easingCurve;

        public WallBossMove(ActorComponent controlledHero, Vector3 positionToGoTo, WallBossMovement movementParams) : base(controlledHero)
        {
            _moveLenght = (ControlledActor.transform.position - positionToGoTo).magnitude / movementParams.MovementSpeed;
            _startingPosition = ControlledActor.transform.position;
            _finalPosition = new Vector3(positionToGoTo.x, ControlledActor.transform.position.y, positionToGoTo.z);
            _easingCurve = movementParams.MovementCurve;
            _setupTime = movementParams.MovementDelay;
        }

        public override void HandleInput(float deltaTime)
        {
            base.HandleInput(deltaTime);

            if (StateTime <= _setupTime) return;

            _t += deltaTime / _moveLenght;

            ControlledActor.transform.position = Vector3.LerpUnclamped(_startingPosition, _finalPosition, _easingCurve.Evaluate(_t));

            if (_t >= 1)
            {
                EndMovement();
            }
        }

        private void EndMovement()
        {
            ControlledActor.transform.position = _finalPosition;
            ControlledActor.ToPreviousState();
        }

        public override InputStates StateId
        {
            get
            {
                return InputStates.Paused;
            }
        }
    }
}
