using System.ComponentModel.Design;
using fi.tamk.hellgame.character;

namespace fi.tamk.hellgame.interfaces
{
    public interface IBossPhase
    {
        void OnEnterPhase();
        void OnExitPhase();
        void OnUpdate(float deltaTime);
        void OnMinionDeath(MinionComponent whichMinion);
        void OnBossHealthChange(float healthPercentage, int hitpoints, int maxHp);
    }
}