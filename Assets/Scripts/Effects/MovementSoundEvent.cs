using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSoundEvent : MonoBehaviour {
    [EventRef]
    public String MovementSound = "";
    private FMOD.Studio.EventInstance _movementSoundLoop;
    private Vector3 _previousMovedPosition;
    private bool hasMoved = false;
    private bool playingSound = false;

    private void Start()
    {
        if (!string.IsNullOrEmpty(MovementSound))
        {
            _movementSoundLoop = FMODUnity.RuntimeManager.CreateInstance(MovementSound);
            var attributes = FMODUnity.RuntimeUtils.To3DAttributes(transform);
            _movementSoundLoop.set3DAttributes(attributes);
        } else
        {
            this.enabled = false;
            Destroy(this);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (!hasMoved)
        {
            if (playingSound)
            {
                _movementSoundLoop.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                playingSound = false;
            }
        }


		if ((_previousMovedPosition - transform.position).sqrMagnitude >= 0.00001f)
        {
            _previousMovedPosition = transform.position;
            hasMoved = true;
            if (!playingSound)
            {
                _movementSoundLoop.start();
                playingSound = true;
            }
        } else
        {
            hasMoved = false;
        }
	}

    private void OnDestroy()
    {
        if (_movementSoundLoop != null)
        {
            _movementSoundLoop.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            _movementSoundLoop.release();
        }
    }
}
