using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using UnityEngine;

namespace fi.tamk.hellgame.phases.bossone
{
    public class PhaseThree : AbstractPhase
    {
        public override void OnUpdate(float deltaTime)
        {
            base.OnUpdate(deltaTime);
            if (PhaseTime > 8f)
            {
                Debug.Log("Boss' energy is exhausted and it perishes");
                Master.TrackedHealth.Die();
            }
        }

        public PhaseThree(BossComponent master) : base(master)
        {
            if (Master.BossActor != null) Master.BossActor.RequestStateChange(InputStates.BossOneFrenzy);
        }
    }
}