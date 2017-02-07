using fi.tamk.hellgame.utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.world
{
    public class RespawnPoint : MonoBehaviour
    {
        void Awake()
        {
            ServiceLocator.Instance.RegisterRespawnPoint(this);
        }

        void OnDestroy()
        {
            if (!ServiceLocator.Quitting) ServiceLocator.Instance.UnregisterRespawnPoint(this);
        }
    }
}
