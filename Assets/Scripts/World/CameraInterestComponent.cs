using fi.tamk.hellgame.utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.world
{

    public class CameraInterestComponent : MonoBehaviour
    {
        [SerializeField] private float cameraInterestStrenght;

        public void Activate()
        {
            ServiceLocator.Instance.MainCameraScript.AddInterest(new CameraInterest(transform, cameraInterestStrenght));
        }

        public void Disable()
        {
            ServiceLocator.Instance.MainCameraScript.RemoveInterest(transform);
        }

        private void OnDestroy()
        {
            if (SceneLoadLock.SceneChangeInProgress) return;

            if (!ServiceLocator.Quitting) Disable();
        }
    }
}
