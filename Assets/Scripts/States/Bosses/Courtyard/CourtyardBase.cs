using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.utils;
using fi.tamk.hellgame.world;
using UnityEngine;

namespace fi.tamk.hellgame.states
{
    public abstract class CourtyardBase : StateAbstract
    {
        protected enum ExternalLabel : int
        {
            Gun = 0,
            Pivot = 1,
            Pentagram = 2,
        }

        protected enum ExternalFloats : int
        {
            FollowRotationSpeed         = 0,
            AutoRotationSpeed           = 1,
            IntermissionDuration        = 2
        }

        protected Transform GunPivot = null;
        protected Transform GunTarget = null;
        protected BossExternalObjects Externals = null;
        protected float RotationSpeed ;
        protected FairTrailHazardRail[] Rails = null;

        protected HealthComponent Health;

        protected float TransitionPercentage;

        public override InputStates StateId
        {
            get
            {
                return InputStates.CourtyardBasicFire;
            }
        }

        protected CourtyardBase(ActorComponent controlledHero, CourtyardBase clonedState = null) : base(controlledHero)
        {
            if (clonedState == null)
            {
                Externals = controlledHero.gameObject.GetComponent<BossExternalObjects>();
                GunPivot = Externals.ExistingGameObjects[(int)ExternalLabel.Pivot].transform;
                Health = controlledHero.gameObject.GetComponent<HealthComponent>();
                RotationSpeed = controlledHero.ActorNumericData.ActorFloatData[(int)ExternalFloats.FollowRotationSpeed];
                Rails = Externals.ExistingGameObjects[(int)ExternalLabel.Pentagram].GetComponentsInChildren<FairTrailHazardRail>();
            }
            else
            {
                GunPivot = clonedState.GunPivot;
                GunTarget = clonedState.GunTarget;
                Externals = clonedState.Externals;
                Health = clonedState.Health;
                RotationSpeed = clonedState.RotationSpeed;
                Rails = clonedState.Rails;
                TransitionPercentage = clonedState.TransitionPercentage;
            }
        }

        protected void FindTarget()
        {
            GunTarget = ServiceLocator.Instance.GetNearestPlayer(ControlledActor.gameObject.transform.position);
        }

        protected void MoveGun()
        {
            if (GunTarget == null) FindTarget();

            if (GunTarget != null && GunPivot != null)
            {
                var targetRotation = Quaternion.LookRotation(new Vector3(GunTarget.position.x, GunPivot.position.y, GunTarget.position.z) - GunPivot.position);
                GunPivot.rotation = Quaternion.Slerp(GunPivot.rotation, targetRotation, RotationSpeed * WorldStateMachine.Instance.DeltaTime);
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

        protected virtual void HailSatan()
        {
            foreach (var rail in Rails)
            {
                rail.PlayTheFlame();
            }
        }
    }
}
