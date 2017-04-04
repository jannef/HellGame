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
        private float _pentagramTimer = 0f;
        private float _pentagramDuration;

        private readonly CourtyardBase _nextState;
        private readonly ParticleSystem _shield;

        public CourtyardIntermission(ActorComponent controlledHero, CourtyardBase followingState, CourtyardBase clonedState = null) : base(controlledHero, clonedState)
        {
            _duration = controlledHero.ActorNumericData.ActorFloatData[(int)ExternalFloats.IntermissionDuration];
            _nextState = followingState;
            _pentagramDuration = _duration / (HowManyPentagrams + 1);
            _pentagramTimer = _duration;

            var shaker = ControlledActor.ActorNumericData.ReferenceCache[0] as AngryShakeEffect;
            if (shaker != null) shaker.Activate(_duration);

            _shield = ControlledActor.ActorNumericData.ReferenceCache[1] as ParticleSystem;
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

        public override void OnEnterState()
        {
            if (_shield != null) _shield.Play();
        }

        public override void OnResumeState()
        {
            OnEnterState();
        }

        public override void OnExitState()
        {
            if (_shield != null) _shield.Stop();
        }

        public override void OnSuspendState()
        {
            OnExitState();
        }
    }
}
