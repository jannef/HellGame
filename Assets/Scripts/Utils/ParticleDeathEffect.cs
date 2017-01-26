using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using fi.tamk.hellgame.effects;
using fi.tamk.hellgame.character;
using System;
using fi.tamk.hellgame.utils.Stairs.Utils;

namespace fi.tamk.hellgame.effects
{
    public class ParticleDeathEffect : AbstractDeathEffect
    {
        [SerializeField]
        private GameObject ParticleSystemPrefab;

        public override void Activate()
        {
            GameObject PE = Pool.Instance.GetObject(ParticleSystemPrefab, true);
            PE.transform.position = this.transform.position;
        }
    }
}
