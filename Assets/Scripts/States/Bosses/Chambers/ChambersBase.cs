using UnityEngine;
using System.Collections;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.dataholders;
using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.utils;
using fi.tamk.hellgame.world;
using UnityEngine.AI;

namespace fi.tamk.hellgame.states
{
    public abstract class ChambersBase : StateAbstract
    {
        protected enum FloatDataLabels
        {
            PhaseOneLaserCooldown      = 0,
            PhaseOneLaserBurstDuration = 1,
            CogBursts                  = 2,
            CogBurstDuration           = 3
        }

        protected BossExternalObjects Externals;
        protected NavMeshAgent NavigationAgent;
        protected HealthComponent Health;
        protected LaserEmitter LaserBeam;
        protected ActorData NumericData;
        protected BulletEmitter CogGun;

        protected ChambersBase(ActorComponent controlledHero, ChambersBase clonedState = null)
            : base(controlledHero)
        {
            if (clonedState == null)
            {
                Externals = ControlledActor.GetComponent<BossExternalObjects>();
                NavigationAgent = ControlledActor.GetComponent<NavMeshAgent>();
                Health = ControlledActor.GetComponent<HealthComponent>();
                LaserBeam = ControlledActor.GetComponentInChildren<LaserEmitter>();
                NumericData = ControlledActor.ActorNumericData;
                CogGun = Externals.ExistingGameObjects[0].GetComponent<BulletEmitter>();
            }
            else
            {
                Externals = clonedState.Externals;
                NavigationAgent = clonedState.NavigationAgent;
                Health = clonedState.Health;
                LaserBeam = clonedState.LaserBeam;
                NumericData = clonedState.NumericData;
                CogGun = clonedState.CogGun;
            }
        }

        public override TransitionType CheckTransitionLegality(InputStates toWhichState)
        {
            return TransitionType.LegalOneway;
        }

        public override void OnEnterState()
        {
            Health.HealthChangeEvent += OnHealthChange;
        }

        protected virtual void OnHealthChange(float percentage, int currentHp, int maxHp)
        {

        }

        public override void OnExitState()
        {
            Health.HealthChangeEvent -= OnHealthChange;
        }

        protected void DefaultLaserBurst()
        {
            ControlledActor.StartCoroutine(
                LaserBurst(NumericData.ActorFloatData[(int)FloatDataLabels.PhaseOneLaserBurstDuration]));
        }

        protected IEnumerator LaserBurst(float duration = 6f)
        {
            var timer = 0f;
            var stop = LaserBeam.FireUntilFurtherNotice();

            while (timer < duration)
            {
                timer += WorldStateMachine.Instance.DeltaTime;
                yield return null;
            }

            stop.Invoke();
        }
    }
}
