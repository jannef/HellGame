using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.world;
using fi.tamk.hellgame.dataholders;
using UnityEngine;

namespace fi.tamk.hellgame.states
{
    public class WallBossAbstract : StateAbstract
    {
        public struct WallBossAbstractValues
        {
            public int phaseNumber;
            public int currentPositionIndex;
            public PatrolWayPoint wayPointList;
            public float MaxHealth;

            public WallBossAbstractValues(PatrolWayPoint wayPointList, float MaxHealth)
            {
                this.phaseNumber = 0;
                this.currentPositionIndex = 0;
                this.wayPointList = wayPointList;
                this.MaxHealth = MaxHealth;
            }
        }

        protected WallBossAbstractValues BaseValues;

        public WallBossAbstract(ActorComponent controlledHero, WallBossAbstractValues values) : base(controlledHero)
        {
            BaseValues = values;
        }

        public WallBossAbstract(ActorComponent controlledHero) : base(controlledHero)
        {
            var externalObjects = ControlledActor.gameObject.GetComponent<BossExternalObjects>();
            var varPointList = externalObjects.ExistingGameObjects[3].GetComponent<PatrolWayPoint>();

            var hc = ControlledActor.GetComponent<HealthComponent>();
            BaseValues = new WallBossAbstractValues(varPointList , hc.MaxHp);
        }

        public override InputStates StateId
        {
            get
            {
                return InputStates.WallBoss;
            }
        }

        public override TransitionType CheckTransitionLegality(InputStates toWhichState)
        {
            if (toWhichState == InputStates.WallBossTransition)
            {
                return TransitionType.LegalOneway;
            }

            return base.CheckTransitionLegality(toWhichState);
        }

        private WallBossTransitionPhaseStats GetTransitionStats(int phase)
        {
            Debug.Log(phase);

            var bossExternalObjects = ControlledActor.GetComponent<BossExternalObjects>();

            var stats = UnityEngine.Object.Instantiate(bossExternalObjects.ScriptableObjects[9 + phase]) as WallBossTransitionPhaseStats;

            return stats;
        }

        public override bool TakeDamage(int howMuch, ref int health, ref bool flinch)
        {
            if (BaseValues.phaseNumber < 3)
            {
                if (health <= BaseValues.MaxHealth - (.33f * BaseValues.MaxHealth * (BaseValues.phaseNumber + 1)))
                {
                    ControlledActor.GoToState(new WallBossPhaseTransition(ControlledActor, BaseValues, GetTransitionStats(BaseValues.phaseNumber)));
                }
            }

            

            return base.TakeDamage(howMuch, ref health, ref flinch);
        }
    }
}
