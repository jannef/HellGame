using fi.tamk.hellgame.utils.Stairs.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Character
{
    class PickUpDestroyComponent : MonoBehaviour
    {
        [SerializeField] private LayerMask HitLayer;

        protected void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (HitLayer != (HitLayer | (1 << hit.gameObject.layer))) return;
            Pool.DelayedDestroyGo(gameObject);
        }
    }
}
