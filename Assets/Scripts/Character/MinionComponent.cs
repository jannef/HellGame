using System;
using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.utils;
using fi.tamk.hellgame.utils.Stairs.Utils;
using UnityEngine.Events;

namespace fi.tamk.hellgame.character
{
    public delegate void MinionDeathDelegate(MinionComponent whichMinion);

    /// <summary>
    /// Actor that we want boss to track somehow.
    /// </summary>
    public class MinionComponent : ActorComponent
    {
        public event MinionDeathDelegate MinionDeathEvent;
        private HealthComponent _health;

        protected override void Awake()
        {
            base.Awake();

            // non-null return value means this actor has health component.
            _health = Pool.Instance.GetHealthComponent(gameObject);
            if (_health != null)
            {
                _health.DeathEffect.AddListener(OnMinionDeath);
            }
        }

        protected void OnMinionDeath()
        {
            if (MinionDeathEvent != null) MinionDeathEvent.Invoke(this);
            if (_health != null) _health.DeathEffect.RemoveListener(OnMinionDeath);
        }

        public void DestroyThisMinion(bool triggarsMinionDeathEvents = true)
        {
            if (!triggarsMinionDeathEvents) MinionDeathEvent = null;

            if (_health != null)
            {
                _health.Die();
            }
            else
            {
                // Ghetto way of dying without health component
                Pool.DelayedDestroyGo(gameObject);
            }
        }
    }
}