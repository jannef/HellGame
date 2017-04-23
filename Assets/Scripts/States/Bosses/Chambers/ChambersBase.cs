using UnityEngine;
using System.Collections;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.dataholders;
using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.utils;
using fi.tamk.hellgame.world;
using fi.tamk.hellgame.effectors;
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
            CogBurstDuration           = 3,
            PhaseOneEnd                = 4,
            HitsToDrop                 = 5
        }

        protected BossExternalObjects Externals;
        protected NavMeshAgent NavigationAgent;
        protected HealthComponent Health;
        protected LaserEmitter LaserBeam;
        protected ActorData NumericData;
        protected BulletEmitter CogGun;
        protected CollectiableDropEffect Dropper;
        protected readonly Transform PlayerTransform;

        protected int HitCounter = 0;

        // Navigation agent data.
        protected Vector3 StartPosition;
        protected float StartSpeed;

        protected ChambersBase(ActorComponent controlledHero, ChambersBase clonedState = null)
            : base(controlledHero)
        {
            PlayerTransform = ServiceLocator.Instance.GetNearestPlayer();

            if (clonedState == null)
            {
                Externals = ControlledActor.GetComponent<BossExternalObjects>();
                NavigationAgent = ControlledActor.GetComponent<NavMeshAgent>();
                Health = ControlledActor.GetComponent<HealthComponent>();
                LaserBeam = ControlledActor.GetComponentInChildren<LaserEmitter>();
                NumericData = ControlledActor.ActorNumericData;
                CogGun = Externals.ExistingGameObjects[0].GetComponent<BulletEmitter>();
                StartPosition = ControlledActor.transform.position;
                StartSpeed = NavigationAgent.speed;
                Dropper = ControlledActor.GetComponent<CollectiableDropEffect>();
            }
            else
            {
                Externals = clonedState.Externals;
                NavigationAgent = clonedState.NavigationAgent;
                Health = clonedState.Health;
                LaserBeam = clonedState.LaserBeam;
                NumericData = clonedState.NumericData;
                CogGun = clonedState.CogGun;
                StartPosition = clonedState.StartPosition;
                StartSpeed = clonedState.StartSpeed;
                Dropper = clonedState.Dropper;
                HitCounter = clonedState.HitCounter;
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
            if (++HitCounter >= (int) NumericData.ActorFloatData[(int) FloatDataLabels.HitsToDrop])
            {
                HitCounter = 0;
                Dropper.Activate();
                SpawnTrapOnPlayer();
            }
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

        /// <summary>
        /// Sets trajectory for cogs towards given transform.
        /// </summary>
        /// <param name="target">Target transform.</param>
        /// <param name="velocity">Less than 0 means keep current magnitude.</param>
        protected void CogsToTarget(Transform target, float velocity = -1)
        {
            CogGun.BulletSystem.OneshotBehaviour(0, false, target, velocity);
        }

        protected void StopCogs()
        {
            CogGun.BulletSystem.OneshotBehaviour(1, false);
        }

        protected void SpawnTrapOnPlayer()
        {
            Object.Instantiate(Externals.PrefabsUsed[0], PlayerTransform.position, Quaternion.identity);
        }
    }
}
