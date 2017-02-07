using System;
using fi.tamk.hellgame.interfaces;
using UnityEngine;

namespace fi.tamk.hellgame.character
{
    public enum BossToInitialize
    {
        BossOne        = 1,
        BlobOne         = 2
    }

    [RequireComponent(typeof(BossComponent))]
    public class BossPhaseInitializer : MonoBehaviour
    {
        public BossToInitialize InitialPhase;

        protected void Awake()
        {
            var boss = GetComponent<BossComponent>();
            IBossPhase phase;
            switch (InitialPhase)
            {
                case BossToInitialize.BossOne:
                    phase = new fi.tamk.hellgame.phases.bossone.PhaseOne(boss);
                    break;
                case BossToInitialize.BlobOne:
                    phase = new phases.BlobFireState(boss);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            boss.EnterPhase(phase);
        }
    }
}