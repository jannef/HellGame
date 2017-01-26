using fi.tamk.hellgame.effects;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.character
{
    public class ScreenShakeDeathEffect : AbstractDeathEffect
    {
        [SerializeField] private float shakeAmount = 5f;
        [SerializeField] private float shakeLenght = 0.25f;

        public override void Activate()
        {
            ScreenShaker.Instance.Shake(shakeAmount, shakeLenght);
        }
    }
}
