using fi.tamk.hellgame.character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fi.tamk.hellgame.effects
{
    public class TurretDeathEffect : DeathEffector
    {
        public float _shakeIntensity;

        public override GenericEffect Die()
        {
            var de = base.Die();
            de.SetOnstart(ScreenShakeEffect, new float[2] { 14.3f, 0.49f });
            return de;
        }
    }
}
