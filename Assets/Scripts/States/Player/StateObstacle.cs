using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;

namespace fi.tamk.hellgame.states
{
    public class StateObstacle : StateAbstract
    {
        public StateObstacle(ActorComponent controlledHero) : base(controlledHero)
        {
        }

        public override InputStates StateId
        {
            get
            {
                return InputStates.Obstacle;
            }
        }

        protected override void CheckForFalling()
        {
        }
    }
}
