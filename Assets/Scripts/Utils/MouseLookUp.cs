using System.Collections;
using System.Collections.Generic;
using fi.tamk.hellgame.utils;
using UnityEngine;

namespace tamk.fi.hellgame.character
{
    public class MouseLookUp : Singleton<MouseLookUp>
    {
        public Vector3 GetMousePosition()
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray.origin, ray.direction, out hit, 200f, LayerMask.GetMask(new[] { Constants.WorldPointRaycastLayerName })))
            {
                return hit.point;
            }

            return Vector3.zero;

        }
    }
}
