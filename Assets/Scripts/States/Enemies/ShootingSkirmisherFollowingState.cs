using UnityEngine;
using fi.tamk.hellgame.character;

namespace fi.tamk.hellgame.states
{
    public class ShootingSkirmisherFollowingState : ShootingSkirmisherAbstract
    {
       
        public ShootingSkirmisherFollowingState(ActorComponent ac, ShootingSkirmisherAbstract shootingSkirmisher = null) : base(ac, shootingSkirmisher)
        {
        }

        public override void HandleInput(float deltaTime)
        {
            base.HandleInput(deltaTime);

            if (TargetTransform != null) Agent.destination = TargetTransform.transform.position;

            if (CurrentDistanceToPlayer <= ControlledActor.ActorNumericData.ActorFloatData[1])
            {
                if (CurrentDistanceToPlayer <= ControlledActor.ActorNumericData.ActorFloatData[0])
                {
                    ControlledActor.GoToState(new ShootingSkirmisherRetreatState(ControlledActor, this));
                    return;
                }

                ControlledActor.GoToState(new ShootingSkirmisherStoppedState(ControlledActor, this));
            }
        }

        public override void OnExitState()
        {
            base.OnExitState();
        }
    }
}
