using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using UnityEngine;

namespace fi.tamk.hellgame.states
{
    public class StatePickUpFollowing : StateAbstract
    {
        private float _followDuration;
        private Vector3 _originalPos;
        private Transform _target;

        public StatePickUpFollowing(float duration, Transform whatToFollow,  ActorComponent controlledHero) : base(controlledHero)
        {
            _followDuration = duration;
            _originalPos = ControlledActor.transform.position;
            _target = whatToFollow;
        }

        public override InputStates StateID
        {
            get { return InputStates.PickUpFollowing; }
        }

        public override void HandleInput(float deltaTime)
        {
            base.HandleInput(deltaTime);

            var ratio = StateTimer / _followDuration;
            ControlledActor.transform.position = Vector3.Lerp(_originalPos, _target.position, ratio);

            if (_followDuration < StateTimer) Object.Destroy(ControlledActor.gameObject);
        }
    }
}