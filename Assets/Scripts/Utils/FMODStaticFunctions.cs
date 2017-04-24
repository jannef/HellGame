using fi.tamk.hellgame.utils;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FMODStaticFunctions {

	public static void ApplyMenuSoundState()
    {
        FMOD.Studio.Bus _sfxSoundBus = RuntimeManager.GetBus("bus:/sfx");
        FMOD.Studio.Bus _musicSoundBus = RuntimeManager.GetBus("bus:/music");
        FMOD.Studio.Bus _uiSoundBus = RuntimeManager.GetBus("bus:/ui");

        _sfxSoundBus.setPaused(true);
        _uiSoundBus.setPaused(false);
    }

    public static void ApplyGamePlaySoundState()
    {
        FMOD.Studio.Bus _sfxSoundBus = RuntimeManager.GetBus("bus:/sfx");
        FMOD.Studio.Bus _musicSoundBus = RuntimeManager.GetBus("bus:/music");
        FMOD.Studio.Bus _uiSoundBus = RuntimeManager.GetBus("bus:/ui");

        _sfxSoundBus.setPaused(false);
        _uiSoundBus.setPaused(true);
    }
}
