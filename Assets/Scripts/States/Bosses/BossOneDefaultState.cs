using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;

namespace fi.tamk.hellgame.states
{
    public class BossOneDefaultState : StateAbstract
    {
        public override void HandleInput(float deltaTime)
        {
            ControlledActor.FireGunByIndex(0);
        }

        public BossOneDefaultState(ActorComponent controlledHero) : base(controlledHero)
        {
        }

        public override InputStates StateId
        {
            get { return InputStates.BossOneDefault; }
        }

        public override bool RequestStateChange(InputStates requestedState)
        {
            if (requestedState != InputStates.BossOneFrenzy) return false;
            ControlledActor.GoToState(new BossOneFrenzyState(ControlledActor));
            return true;
        }
    }
}