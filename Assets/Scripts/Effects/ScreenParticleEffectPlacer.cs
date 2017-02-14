using System.Collections;
using System.Collections.Generic;
using fi.tamk.hellgame.utils;
using UnityEngine;

namespace fi.tamk.hellgame.effects
{
    public class ScreenParticleEffectPlacer : Singleton<ScreenParticleEffectPlacer>
    {

        public void PlaceParticleEffectInfrontOfCamera(Transform effectTransform, float distanceFromCamera)
        {
            effectTransform.position = transform.position + transform.forward * distanceFromCamera;
            effectTransform.forward = -transform.forward;
            effectTransform.parent = transform;
        }
    }
}
