using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.character
{
    public delegate void PlayerCollectPointsEvent(int howManyPoints);

    public class PlayerLimitBreak : MonoBehaviour
    {
        public event PlayerCollectPointsEvent powerUpGained;

        public void GainPoints(int howMany)
        {
            
        }
    }
}
