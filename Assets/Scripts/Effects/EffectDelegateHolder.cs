using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.effects
{
    public struct EffectDelegateHolder
    {
        public Runnable runnableDelegate;
        public float[] args;

        public EffectDelegateHolder(Runnable runnableDelegate, float[] args)
        {
            this.runnableDelegate = runnableDelegate;
            this.args = args;
        }
    }
}
