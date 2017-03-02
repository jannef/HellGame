using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace fi.tamk.hellgame.effector
{
    class SlowDownAndScreenShakeEffector : Effector
    {
        [SerializeField] private float _shakeIntensity;
        [SerializeField] private float _shakeLenght;
        [SerializeField] private float _slowDownLenght;
        [SerializeField] private float _slowDownRate;

        public override void Activate()
        {
            base.Activate();
            Effect.SetOnstart(SlowDown, new float[2] { _slowDownLenght, _slowDownRate });
            Effect.SetOnstart(ScreenShakeEffect, new float[2] { _shakeIntensity, _shakeLenght });
        }
    }
}
