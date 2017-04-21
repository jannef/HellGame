using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLimitBreakSoundEffect : MonoBehaviour {
    [FMODUnity.EventRef]
    public String LimitBreakSoundEvent = "";
    private FMOD.Studio.EventInstance _limitBreakSound;

    public void StartSound()
    {
        _limitBreakSound = FMODUnity.RuntimeManager.CreateInstance(LimitBreakSoundEvent);
        _limitBreakSound.setParameterValue("LimitBreakStatus", 1f);
        _limitBreakSound.start();
    }

    public void EndSound()
    {
        if (_limitBreakSound != null)
        {
            _limitBreakSound.setParameterValue("LimitBreakStatus", 0f);
            _limitBreakSound.release();
        }
    }

    private void OnDestroy()
    {
        if (_limitBreakSound != null) _limitBreakSound.release();
    }
}
