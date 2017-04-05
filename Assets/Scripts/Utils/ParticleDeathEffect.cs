using fi.tamk.hellgame.utils;
using UnityEngine;

namespace fi.tamk.hellgame.effects
{
    public class ParticleDeathEffect : GenericEffect
    {
        [SerializeField] private GameObject ParticleSystemPrefab;

        public virtual void Activate()
        {
            GameObject PE = Pool.Instance.GetObject(ParticleSystemPrefab, true);
            PE.transform.position = this.transform.position;
        }
    }
}
