using fi.tamk.hellgame.utils;
using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;

public class PlayerLimitBreakSoundEffect : MonoBehaviour {
    [FMODUnity.EventRef]
    public String LimitBreakSoundEvent = "";
    [FMODUnity.EventRef]
    public String LimitBreakStartsSoundEvent = "";
    [FMODUnity.EventRef]
    public String LimitBreakAvailableSoundEvent = "";
    [FMODUnity.EventRef]
    public String LimitBreakAvailableCrack = "";
    private FMOD.Studio.EventInstance _limitBreakSound;
    private FMOD.Studio.EventInstance _limitBreakAvailable;

    private bool _limitIndicatorPlaying = false;

    private void Awake()
    {
        _limitBreakAvailable = FMODUnity.RuntimeManager.CreateInstance(LimitBreakAvailableSoundEvent);
    }

    private PLAYBACK_STATE GetState(EventInstance @event)
    {
        PLAYBACK_STATE state;
        @event.getPlaybackState(out state);
        return state;
    }

    public void LimitBreakAvailable()
    {
        if (_limitIndicatorPlaying) return;
        _limitIndicatorPlaying = true;

        Utilities.PlayOneShotSound(LimitBreakAvailableCrack, transform.position);
        _limitBreakAvailable.start();
    }

    public void LimitBreakNotAvailable()
    {
        if (!_limitIndicatorPlaying) return;
        _limitBreakAvailable.stop(STOP_MODE.IMMEDIATE);
    }

    public void StartSound()
    {
        LimitBreakNotAvailable();
        _limitBreakSound = FMODUnity.RuntimeManager.CreateInstance(LimitBreakSoundEvent);
        Utilities.PlayOneShotSound(LimitBreakStartsSoundEvent, transform.position);
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
        if (_limitBreakSound != null)
        {
            _limitBreakSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            _limitBreakSound.release();
        }
        if (_limitBreakAvailable != null)
        {
            _limitBreakAvailable.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            _limitBreakAvailable.release();
        }
    }
}
