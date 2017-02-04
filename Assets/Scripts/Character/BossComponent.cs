using System.Collections.Generic;
using fi.tamk.hellgame.interfaces;
using UnityEngine;

namespace fi.tamk.hellgame.character
{
    public class BossComponent : MonoBehaviour
    {
        public float EncounterTimer { get; protected set; }
        public HealthComponent TrackedHealth;
        public ActorComponent BossActor;

        public GameObject[] PrefabsUsedByBoss;

        protected List<MinionComponent> ControlledMinions;
        protected IBossPhase CurrentPhase;

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
            if(CurrentPhase != null) CurrentPhase.OnMinionDeath(whichMinion);
        }

        protected virtual void Update()
        {
            EncounterTimer += Time.deltaTime;
            if(CurrentPhase != null) CurrentPhase.OnUpdate(Time.deltaTime);
        }

        protected virtual void OnBossHealthChange(float percentage, int hitpoints, int maxHp)
        {
            if (CurrentPhase != null) CurrentPhase.OnBossHealthChange(percentage, hitpoints, maxHp);
        }

        public virtual void EnterPhase(IBossPhase toWhichPhase)
        {
            if (toWhichPhase == null) return;
            if (CurrentPhase != null) CurrentPhase.OnExitPhase();
            CurrentPhase = toWhichPhase;
            CurrentPhase.OnEnterPhase();
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