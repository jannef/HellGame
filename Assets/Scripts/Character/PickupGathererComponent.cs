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
        protected UpgradeableBulletEmitter[] BulletEmitters; // TODO: will change to upgradeable weapon when it is implemented

        void Start()
        {
            BulletEmitters = GetComponents<UpgradeableBulletEmitter>();
        }

        public virtual void PickItem(PickupType item)
        {
            switch (item) {
                case PickupType.Basic:
                    foreach(UpgradeableBulletEmitter emitter in BulletEmitters)
                    {
                        emitter.AddUpgradepoints(1);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
