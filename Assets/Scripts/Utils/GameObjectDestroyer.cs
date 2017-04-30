using UnityEngine;
using System.Collections;

namespace fi.tamk.hellgame.utils
{
    public class GameObjectDestroyer : MonoBehaviour
    {
        public void DestroyGameObject(bool immeadite)
        {
            if (immeadite)
            {
                DestroyImmediate(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
