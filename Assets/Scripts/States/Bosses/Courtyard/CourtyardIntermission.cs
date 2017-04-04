using UnityEngine;
using System.Collections;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.effects;
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

            var shaker = ControlledActor.ActorNumericData.ReferenceCache[0] as AngryShakeEffect;
            if (shaker != null) shaker.Activate(_duration);
        }

        public override void HandleInput(float deltaTime)
        {
            base.HandleInput(deltaTime);
            ControlledActor.FireGunByIndex(3);
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

        public override bool TakeDamage(int howMuch, ref int health, ref bool flinch)
        {
            return health > 0;
        }
    }
}
