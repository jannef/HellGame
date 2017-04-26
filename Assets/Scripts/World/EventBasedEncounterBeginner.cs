using fi.tamk.hellgame.world;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBasedEncounterBeginner : MonoBehaviour {

    private bool _hasBeenActivated = false;

    public void BeginEncounter()
    {
        if (!_hasBeenActivated)
        {
            RoomIdentifier.BeginEncounter();
            _hasBeenActivated = true;
        }
    }
}
