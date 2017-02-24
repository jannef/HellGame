using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;

namespace fi.tamk.hellgame.states
{
    public class ShootingSkirmisherRetreatState : ShootingSkirmisherAbstract
    {

        public ShootingSkirmisherRetreatState(ActorComponent ac, ShootingSkirmisherAbstract shootingSkirmisher = null) : base(ac, shootingSkirmisher)
        {
        }

        public override void HandleInput(float deltaTime)
        {
            base.HandleInput(deltaTime);
            if (CurrentDistanceToPlayer > ControlledActor.ActorNumericData.ActorFloatData[0] &&
                (ControlledActor.transform.position - RetreatPoint).magnitude <= ControlledActor.ActorNumericData.ActorFloatData[2])
            {
                ControlledActor.GoToState(new ShootingSkirmisherStoppedState(ControlledActor, this));
                return;
            }

            if (CurrentDistanceToPlayer > ControlledActor.ActorNumericData.ActorFloatData[1])
            {
                ControlledActor.GoToState(new ShootingSkirmisherFollowingState(ControlledActor, this));
            }
        }

        public override void OnEnterState()
        {
            base.OnEnterState();

            Agent.SetDestination(RetreatPoint);
        }

        public override void OnExitState()
        {
        }
    }
}
