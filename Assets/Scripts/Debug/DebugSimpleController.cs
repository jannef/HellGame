using System.Collections;
using fi.tamk.hellgame.effects;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.debug
{
    public class DebugSimpleController : MonoBehaviour
    {
        ScreenShaker shaker;

        // Use this for initialization
        void Start()
        {
            shaker = GetComponent<ScreenShaker>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Jump"))
            {
                shaker.Shake(0.33f, 1.5f);
            }
        }
    }
}
