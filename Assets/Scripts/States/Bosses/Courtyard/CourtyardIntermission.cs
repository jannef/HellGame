using UnityEngine;
using System.Collections;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.world;

namespace fi.tamk.hellgame.states
{
    public class CourtyardIntermission : CourtyardBase
    {
        private const int HowManyPentagrams = 3;
        private float _duration;
        private CourtyardBase _nextState;
        private float _pentagramTimer = 0f;
        private float _pentagramDuration;

        public CourtyardIntermission(ActorComponent controlledHero, CourtyardBase followingState, CourtyardBase clonedState = null) : base(controlledHero, clonedState)
        {
            _duration = controlledHero.ActorNumericData.ActorFloatData[(int)ExternalFloats.IntermissionDuration];
            _nextState = followingState;
            _pentagramDuration = _duration / (HowManyPentagrams + 1);
            _pentagramTimer = _duration;
        }

        public override void HandleInput(float deltaTime)
        {
            base.HandleInput(deltaTime);
            _pentagramTimer += deltaTime;

            if (_pentagramTimer > _pentagramDuration)
            {
                _pentagramTimer = 0f;
                HailSatan();
            }

            if (StateTime > _duration)
            {
                ControlledActor.GoToState(_nextState);
            }
        }

        private IEnumerator FlashPentagramObject(int howManyTimes)
        {
            var timer = 0f;
            var oneFlashDuration = _duration / howManyTimes;

            while (true)
            {
                timer += WorldStateMachine.Instance.DeltaTime;
                if (timer > oneFlashDuration)
                {
                    HailSatan();
                }
            }
        }

        public override bool TakeDamage(int howMuch, ref int health, ref bool flinch)
        {
            return health > 0;
        }
    }
}
