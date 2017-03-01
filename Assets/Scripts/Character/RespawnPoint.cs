using fi.tamk.hellgame.utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.world
{
    public class RespawnPoint : MonoBehaviour
    {
        protected void Awake()
        {
            ServiceLocator.Instance.RegisterRespawnPoint(this);
        }

        protected void OnDestroy()
        {
            if (SceneLoadLock.SceneChangeInProgress) return;
            if (!ServiceLocator.Quitting) ServiceLocator.Instance.UnregisterRespawnPoint(this);
        }
    }
}
