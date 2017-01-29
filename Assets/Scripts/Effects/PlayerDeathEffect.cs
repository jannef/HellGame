using fi.tamk.hellgame.character;
using fi.tamk.hellgame.world;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace fi.tamk.hellgame.effects
{
    public class PlayerDeathEffect : DeathEffector
    {
        [SerializeField] private float _shakeIntensity;
        [SerializeField] private float _shakeLenght;
        [SerializeField] private float _slowDownScale;
        [SerializeField] private float _slowDownLenght;

        public override GenericEffect Activate()
        {
            var de = base.Activate();
            de.SetOnstart(SlowDown, new float[2] { _slowDownLenght, _slowDownScale });
            de.LifeTime = _slowDownLenght * _slowDownScale;
            de.SetOnEnd(ScreenShakeEffect, new float[2] { _shakeIntensity, _shakeLenght });
            return de;
        }
    }
}
