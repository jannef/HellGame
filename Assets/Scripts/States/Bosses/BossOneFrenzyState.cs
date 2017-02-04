using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;

namespace fi.tamk.hellgame.states
{
    public class BossOneFrenzyState : StateAbstract
    {
        public override void HandleInput(float deltaTime)
        {
            ControlledActor.FireGunByIndex(1);
        }

        public BossOneFrenzyState(ActorComponent controlledHero) : base(controlledHero)
        {
        }

        public override InputStates StateId
        {
            get { return InputStates.BossOneFrenzy; }
        }
    }
}