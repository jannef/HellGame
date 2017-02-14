using UnityEngine;

namespace fi.tamk.hellgame.character
{
    public enum PickupType
    {
        Basic       = 1
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
        }

        public virtual void PickItem(PickupType item)
        {
            switch (item) {
                case PickupType.Basic:
                    LimitBreak.GainPoints(1);
                    break;
                default:
                    break;
            }
        }
    }
}
