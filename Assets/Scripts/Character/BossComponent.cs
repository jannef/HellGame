using System.Collections.Generic;
using fi.tamk.hellgame.interfaces;
using UnityEngine;
using System.Linq;
using fi.tamk.hellgame.world;

namespace fi.tamk.hellgame.character
{
    public class BossComponent : MonoBehaviour
    {
        public float EncounterTimer { get; protected set; }
        public HealthComponent TrackedHealth;
        public ActorComponent BossActor;

        public GameObject[] PrefabsUsedByBoss;
        public GameObject[] ExistingObjectsUsedByBoss;
        public ScriptableObject[] ScriptableObjectsUsedByBoss;

        protected List<MinionComponent> ControlledMinions;
        protected List<IBossPhase> CurrentPhase = new List<IBossPhase>();

        public void AddMinion(params MinionComponent[] minionsToAdd)
        {
            foreach (var minionComponent in minionsToAdd)
            {
                minionComponent.MinionDeathEvent += OnMinionDeath;
            }
            ControlledMinions.AddRange(minionsToAdd);
        }

        protected virtual void OnMinionDeath(MinionComponent whichMinion)
        {
            // count dead minions, gather rage or something
            if (CurrentPhase == null || CurrentPhase.Count == 0) return;
            CurrentPhase.ForEach(x => x.OnMinionDeath(whichMinion));
        }

        protected virtual void Update()
        {
            EncounterTimer += WorldStateMachine.Instance.DeltaTime;
            if (CurrentPhase == null || CurrentPhase.Count == 0) return;
            CurrentPhase.ForEach(x => x.OnUpdate(WorldStateMachine.Instance.DeltaTime));
        }

        protected virtual void OnBossHealthChange(float percentage, int hitpoints, int maxHp)
        {
            if (CurrentPhase == null || CurrentPhase.Count == 0) return;
            CurrentPhase.ForEach(x => x.OnBossHealthChange(percentage, hitpoints, maxHp));
        }

        public virtual void EnterPhase(IBossPhase toWhichPhase)
        {
            if (toWhichPhase == null || CurrentPhase.Count(x => x == toWhichPhase) > 0) return;
            CurrentPhase.Add(toWhichPhase);
        }

        public virtual void RemovePhase(IBossPhase whichPhase)
        {
            CurrentPhase.RemoveAll(x => x == whichPhase);
        }

        public virtual void EndAllPhases(IBossPhase soleSurvivor = null)
        {
            CurrentPhase.RemoveAll(x => x != soleSurvivor);
        }

        /// <summary>
        /// If tracked health can be determined for this boss, subscribe to health change events
        /// of that health component.
        /// </summary>
        private void Awake()
        {
            if (TrackedHealth == null) TrackedHealth = GetComponent<HealthComponent>();
            if (TrackedHealth != null)
            {
                TrackedHealth.HealthChangeEvent += OnBossHealthChange;
            }

            if (BossActor == null) BossActor = GetComponent<ActorComponent>();
        }
    }
}