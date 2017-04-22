using fi.tamk.hellgame.world;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterBeginner : MonoBehaviour {
    [SerializeField] private float MaxDelayAtStart = 5f;
    private float AllowInputInterruptionDelayLength = 0.25f;
    float timer = 0f;
    private bool _hasBeenActivated = false;
    private bool _hasDetectedInput = false;
	
	void Update()
    {
        timer += Time.deltaTime;

        if (Input.anyKeyDown)
        {
            _hasDetectedInput = true;
        }

        if (_hasDetectedInput && timer >= AllowInputInterruptionDelayLength)
        {
           if (!_hasBeenActivated) RoomIdentifier.BeginEncounter();
            _hasBeenActivated = true;
            this.enabled = false;
        }

        if (timer >= MaxDelayAtStart)
        {
            if (!_hasBeenActivated) RoomIdentifier.BeginEncounter();
            _hasBeenActivated = true;
            this.enabled = false;
        }
    }
}
