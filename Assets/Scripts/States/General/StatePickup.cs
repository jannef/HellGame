using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.states;
using fi.tamk.hellgame.utils.Stairs.Utils;
using UnityEngine;

namespace fi.tamk.hellgame.projectiles
{
    public class PickupState : StateAbstract
    {
        public PickupState(ActorComponent controlledHero) : base(controlledHero)
        {
        }

        public override InputStates StateID
        {
            get { return InputStates.PickUp; }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (OnPickUp(other))
            {
                ControlledActor.GoToState(new StatePickUpFollowing(.33f, other.transform, ControlledActor));
            }

        }

        public override void OnEnterState()
        {
            ControlledActor.OnTriggerEnterActions += OnTriggerEnter;
        }

        public override void OnExitState()
        {
            ControlledActor.OnTriggerEnterActions -= OnTriggerEnter;
        }

        public override void OnResumeState()
        {
            ControlledActor.OnTriggerEnterActions += OnTriggerEnter;
        }

        public override void OnSuspendState()
        {
            ControlledActor.OnTriggerEnterActions -= OnTriggerEnter;
        }

        /// <summary>
        /// Run when another object enters this trigger collider.
        ///
        /// Overload this method on derived types to customize effects of pickups.
        /// </summary>
        /// <param name="other">Collision data passed down from unity</param>
        /// <returns>If this collision should qualify as pickup. False ignores it and leves this
        /// object be.</returns>
        protected virtual bool OnPickUp(Collider other)
        {
            // buffs or debuffs to colliding other...
            return true;
        }
    }
}
