using UnityEngine;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;

namespace fi.tamk.hellgame.phases
{
    public class AbstractPhase : IBossPhase
    {
        protected float PhaseTime = 0f;
        protected BossComponent Master;

        public virtual void OnEnterPhase()
        {
            //Debug.Log(string.Format("{0}.OnEnterPhase", GetType()));
        }

        public virtual void OnExitPhase()
        {
            //Debug.Log(string.Format("{0}.OnExitPhase", GetType()));
        }

        public virtual void OnUpdate(float deltaTime)
        {
            PhaseTime += deltaTime;
        }

        public virtual void OnMinionDeath(MinionComponent whichMinion)
        {
            //Debug.Log(string.Format("{0}.OnMinionDeath: {1}", GetType(), whichMinion.gameObject));
        }

        public virtual void OnBossHealthChange(float healthPercentage, int hitpoints, int maxHp)
        {
            //Debug.Log(string.Format("{0}.OnBossHealthChange: {1}%", GetType(), healthPercentage*100f));
        }

        public AbstractPhase(BossComponent master)
        {
            Master = master;
        }
    }
}