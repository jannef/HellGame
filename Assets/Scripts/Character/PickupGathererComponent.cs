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
        protected BulletEmitter[] BulletEmitters; // TODO: will change to upgradeable weapon when it is implemented

        public virtual void PickItem(PickupType item)
        {
            // Do shit;
        }
    }
}
