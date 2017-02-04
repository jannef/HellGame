using fi.tamk.hellgame.interfaces;
using UnityEngine;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.utils;

namespace fi.tamk.hellgame.states
{
    public abstract class StateAbstract : IInputState
    {
        public ActorComponent ControlledActor { get; private set; }
        protected float DamageMultiplier = 1f;

        public CharacterController HeroAvatar
        {
            get { return _heroAvatar ?? (_heroAvatar = ControlledActor.CharacterController); }
        }
        private CharacterController _heroAvatar;

        protected float StateTime;

        public abstract InputStates StateId { get; }

        public virtual float StateTimer
        {
            get
            {
                return StateTime;
            }
        }

        public virtual bool RequestStateChange(InputStates requestedState)
        {
            return false;
        }

        public virtual TransitionType CheckTransitionLegality(InputStates toWhichState)
        {
            if (toWhichState == StateId) return TransitionType.Illegal;
            return TransitionType.LegalTwoway;
        }

        public virtual void HandleInput(float deltaTime)
        {
            StateTime += deltaTime;
            CheckForFalling();
        }

        public virtual void OnEnterState()
        {
        }

        public virtual void OnExitState()
        {
        }

        public virtual void OnResumeState()
        {
        }

        public virtual void OnSuspendState()
        {
        }

        protected virtual void CheckForFalling()
        {
            var ray = new Ray(ControlledActor.transform.position + Vector3.up, Vector3.down);
            if (!Physics.Raycast(ray, 10f, LayerMask.GetMask(Constants.GroundRaycastLayerName)))
            {
                ControlledActor.GoToState(new StateFalling(ControlledActor));
            }
        }

        protected StateAbstract(ActorComponent controlledHero)
        {
            StateTime = 0f;
            ControlledActor = controlledHero;
        }

        public virtual bool TakeDamage(int howMuch, ref int health, ref bool flinch)
        {
            var damage = (int) (howMuch * DamageMultiplier);
            if (damage > 0) flinch = true;
            health -= damage;
            return health > 0;
        }
    }
}
