
using fi.tamk.hellgame.character;

namespace fi.tamk.hellgame.states
{
    public class CourtyardDeathPhase : CourtyardBase
    {
        public CourtyardDeathPhase(ActorComponent controlledHero, CourtyardBase clonedState = null) : base(controlledHero, clonedState)
        {
        }

        public override void HandleInput(float deltaTime)
        {
            base.HandleInput(deltaTime);
        }
    }
}
