using System.Collections;
using System.Collections.Generic;
using fi.tamk.hellgame.effector;
using UnityEngine;

namespace fi.tamk.hellgame.effectors
{
    public class LimitBreakEffector : PlayerEffector
    {
        [SerializeField] private PlayerLimitBreakStats _limitBreakStats;

        private void Start()
        {
            _effectLength = _limitBreakStats.LimitBreakLenght;
        }

        public override void Activate()
        {
            base.Activate();
        }
    }
}
