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

        public static Transform[] GetAllTranformsFromChildren(this GameObject go)
        {
            var transformArray = go.GetComponentsInChildren<Transform>();
            Transform[] _returnTranforms;
            if (transformArray.Length <= 1)
            {
                _returnTranforms = null;
            }
            else
            {
                _returnTranforms = new Transform[transformArray.Length-1];

                // This is used to remove the parent from available spawnPoints.
                var availableSpawnPointIndex = 0;
                foreach (var t in transformArray)
                {
                    if (t.GetInstanceID() != go.transform.GetInstanceID()) _returnTranforms[availableSpawnPointIndex++] = t;
                }
            }

            return _returnTranforms;
        }
    }
}
