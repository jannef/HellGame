using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace fi.tamk.hellgame.utils
{
    public struct CameraInterest
    {
        public Transform InterestTransform;
        public float InterestWeight;

        public CameraInterest(Transform transform, float weight)
        {
            InterestWeight = weight;
            InterestTransform = transform;
        }
    }
}
