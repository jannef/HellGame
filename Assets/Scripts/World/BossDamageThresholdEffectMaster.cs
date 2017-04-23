using fi.tamk.hellgame.character;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace fi.tamk.hellgame.world
{

    public class BossDamageThresholdEffectMaster : MonoBehaviour
    {
        [SerializeField] private HealthComponent _bossSystemToFollow;
        [SerializeField] private float _wakeDelay = 0.11f;

        private BossDamageThresholdEffect[] _children;
        private int _popIterator = 0;

        private float _threshold;
        private float _nextPop;

        private void Awake()
        {
            if (_bossSystemToFollow != null) _bossSystemToFollow.HealthChangeEvent += TakeDamage;

            _children = GetComponentsInChildren<BossDamageThresholdEffect>();
            foreach (var c in _children)
            {
                c.Initialize();
            }

            _children = _children.OrderBy(x => x.Priority).ToArray();
            _threshold = 1f / (_children.Length + 1);
            _nextPop = 1f - _threshold;
        }

        public void WakeUp()
        {
            StartCoroutine(WakeCycle());
        }

        private IEnumerator WakeCycle()
        {
            foreach (var t in _children)
            {
                var timer = 0f;
                while (timer < _wakeDelay)
                {
                    timer += WorldStateMachine.Instance.DeltaTime;
                    yield return null;
                }
                t.Wake();
            }
        }

        // Use this for initialization
        private void TakeDamage(float percentage, int hp, int max)
        {
            if (!(percentage <= _nextPop)) return;
            _nextPop -= _threshold;
            _children[_popIterator++].Pop();
        }
    }
}
