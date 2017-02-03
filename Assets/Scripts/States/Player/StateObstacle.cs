using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;

namespace fi.tamk.hellgame.states
{
    public class StateObstacle : StateAbstract
    {
        public StateObstacle(ActorComponent controlledHero) : base(controlledHero)
        {
        }

        public override InputStates StateID
        {
            get
            {
                return InputStates.Obstacle;
            }
        }

        protected override void CheckForFalling()
        {
        }

        public override void OnEnterState()
        {
            base.OnEnterState();
            HeroAvatar.enabled = false;
            ControlledActor.enabled = false;
        }
    }
}
