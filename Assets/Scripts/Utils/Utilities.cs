using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace fi.tamk.hellgame.utils
{
    public static class Utilities
    {
        public static LayerMask GetFiringLayer(LayerMask firerMask)
        {
            return LayerMask.NameToLayer(firerMask == LayerMask.NameToLayer("Player") ? "PlayerBullet" : "EnemyBullet");
        }
    }
}
