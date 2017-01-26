using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using fi.tamk.hellgame.interfaces;
using System;

namespace fi.tamk.hellgame.effects
{
    public abstract class AbstractDeathEffect : MonoBehaviour, IDeathEffect
    {
        public float StartDelay;

        public abstract void Activate();
    }
}
