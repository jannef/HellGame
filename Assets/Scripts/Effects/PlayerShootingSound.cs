using fi.tamk.hellgame.world;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingSound : MonoBehaviour {

    [FMODUnity.EventRef]
    public String ShootingSoundEffect = "";
    private float _stopTimer;
    [SerializeField] private float _stopLenght = 0.12f;

    private FMOD.Studio.EventInstance _shootingLoop;

    public void Start()
    {
        _shootingLoop = FMODUnity.RuntimeManager.CreateInstance(ShootingSoundEffect);
    }

    public void Shoot()
    {
        _stopTimer = 0f;
        FMOD.Studio.PLAYBACK_STATE isplaying;
        _shootingLoop.getPlaybackState(out isplaying);
        if (isplaying == FMOD.Studio.PLAYBACK_STATE.STOPPED || isplaying == FMOD.Studio.PLAYBACK_STATE.STOPPING)
        {
            _shootingLoop.start();
        }
    }

    private void Update()
    {
        if (_stopTimer >= _stopLenght)
        {
            if (_shootingLoop != null)
            {
                FMOD.Studio.PLAYBACK_STATE isplaying;
                _shootingLoop.getPlaybackState(out isplaying);
                if (isplaying == FMOD.Studio.PLAYBACK_STATE.PLAYING)
                {
                    _shootingLoop.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                }
            }
                
        } else
        {
            _stopTimer += WorldStateMachine.Instance.DeltaTime;
        }
    }

    private void OnDestroy()
    {
        if (_shootingLoop != null) _shootingLoop.release();
    }
}
