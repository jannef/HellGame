using fi.tamk.hellgame.character;
using UnityEngine;

namespace fi.tamk.hellgame.phases.bossone
{
    public class PhaseOne : AbstractPhase
    {
        private HealthComponent _myHealth;

        public override void OnUpdate(float deltaTime)
        {
            base.OnUpdate(deltaTime);
            if (PhaseTime > 10f)
            {
                Master.TrackedHealth.Health = Master.TrackedHealth.MaxHp;
                PhaseTime = 0f;
                Debug.Log("Boss regenerates it's wounds! (Reduce it to 50% within 10s)");
            }
        }

        public override void OnBossHealthChange(float healthPercentage, int hitpoints, int maxHp)
        {
            if (healthPercentage <= 0.5f)
            {
                Debug.Log("Boss brought to 50%, proceeding to phase 2!");
                Master.EnterPhase(new PhaseTwo(Master));
            }
        }

        public PhaseOne(BossComponent master) : base(master)
        {

        }
    }
}
