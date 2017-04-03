using fi.tamk.hellgame.character;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.world
{

    public class BossDamageThresholdEffectMaster : MonoBehaviour
    {
        public event Action<float> DamageEvent;
        [SerializeField] private HealthComponent _bossSystemToFollow;

        private void Start()
        {
            if (_bossSystemToFollow != null) _bossSystemToFollow.HealthChangeEvent += TakeDamage;
            _bossSystemToFollow.DeathEffect.AddListener(DeathEffect);
        }

        private void DeathEffect()
        {
            TakeDamage(0f, 0, 0);
        }

        // Use this for initialization
        void TakeDamage(float percentage, int hp, int max)
        {
            if (DamageEvent != null) DamageEvent.Invoke(percentage);
        }
    }
}
