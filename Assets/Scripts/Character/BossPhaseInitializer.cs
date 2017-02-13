using System;
using fi.tamk.hellgame.interfaces;
using UnityEngine;

namespace fi.tamk.hellgame.character
{
    public enum BossToInitialize
    {
        BossOne        = 1,
        BlobFirstPhase         = 2,
        BlobDropHomingsPhase     = 3,
        BlobDropTurrets = 4,
        BlobSecondFiringPhase = 5,
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
                case BossToInitialize.BlobFirstPhase:
                    phase = new phases.BlobFirePhase(boss);
                    break;
                case BossToInitialize.BlobDropHomingsPhase:
                    phase = new phases.BlowFirstSpanwPhase(boss);
                    break;
                case BossToInitialize.BlobSecondFiringPhase:
                    phase = new phases.BlobSecondFirePhase(boss);
                    break;
                case BossToInitialize.BlobDropTurrets:
                    phase = new phases.BlobSpawnTurretsPhase(boss);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            boss.EnterPhase(phase);
        }
    }
}