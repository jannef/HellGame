using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.world
{
    public class BeginEncounterTrigger : MonoBehaviour
    {

        public void BeginEncounter()
        {
            RoomIdentifier.OnEncounterBegin();
        }
    }
}
