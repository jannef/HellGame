using fi.tamk.hellgame.effector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.effector
{

    public class ScreenShakeEffector : Effector
    {
        [SerializeField] float shakeIntensity;
        [SerializeField] float shakeLenght;

        public override void Activate()
        {
            base.Activate();
            Effect.SetOnstart(ScreenShakeEffect, new float[2] { shakeIntensity, shakeLenght });
        }
    }
}
