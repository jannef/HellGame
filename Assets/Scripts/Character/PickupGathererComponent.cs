using fi.tamk.hellgame.effects;
using UnityEngine;

namespace fi.tamk.hellgame.character
{
    public enum PickupType
    {
        Basic       = 1,
        Health      = 2
    }

    [RequireComponent(typeof(ActorComponent)), RequireComponent(typeof(HealthComponent))]
    public class PickupGathererComponent : MonoBehaviour
    {
        protected ActorComponent Actor;
        protected HealthComponent Health;
        protected PlayerLimitBreak LimitBreak;

        void Start()
        {
            LimitBreak = GetComponent<PlayerLimitBreak>();
            Health = GetComponent<HealthComponent>();
        }

        public virtual void PickItem(PickupType item)
        {
            switch (item) {
                case PickupType.Basic:
                    LimitBreak.GainPoints(1);
                    break;
                case PickupType.Health:
                    Health.Heal(1);
                    break;
                default:
                    break;
            }
        }
    }
}
