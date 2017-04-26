using System;
using UnityEngine;
using System.Collections;
using fi.tamk.hellgame.utils;
using FMOD.Studio;
using FMODUnity;

namespace fi.tamk.hellgame
{
    public class MusicPlayer : MonoBehaviour
    {
        public int NumberOfTracks { get { return _musicTracks.Length; } }
        public bool LoopTrack = true;

        [EventRef] public string[] _music;
        private EventInstance[] _musicTracks;

        private int _iterator = 0;
        private bool _starting = true;

        private void Awake()
        {
            _musicTracks = new EventInstance[_music.Length];
            for (var i = 0; i < _music.Length; i++)
            {
                if (string.IsNullOrEmpty(_music[i]))
                {
                    throw new UnityException("MusicPlayer is misconfigured, it contains invalid FMOD event. This is not allowed.");

                }
                _musicTracks[i] = RuntimeManager.CreateInstance(_music[i]);
                // 3d attributes are omitted here, it makes no sense for music
            }
        }

        private void Update()
        {
            if (_starting)
            {
                _musicTracks[_iterator].start();
                _starting = false;
                return;
            }

            PLAYBACK_STATE state;
            _musicTracks[_iterator].getPlaybackState(out state);

            if (state == PLAYBACK_STATE.STOPPED)
            {
                if (LoopTrack)
                {
                    RestartTrack();
                }
                else
                {
                    NextTrack();
                }
            }

            HandleInput();
        }

        public void PlayTrackByIndex(int index, STOP_MODE stopMode = STOP_MODE.IMMEDIATE)
        {
            if (index < 0 || index >= NumberOfTracks)
            {
                throw new UnityException("Track index out of range!");
            }

            _musicTracks[_iterator].stop(stopMode);
            _iterator = index;
            _musicTracks[_iterator].start();
            Paused = false;
        }

        public void PlayTrackByTrackNumber(int whichTrack)
        {
            try
            {
                PlayTrackByIndex(whichTrack - 1);
            }
            catch (Exception)
            {
                throw new UnityException("Track number out of range!");
            }
            
        }

        public void NextTrack(STOP_MODE stopMode = STOP_MODE.IMMEDIATE)
        {
            _musicTracks[_iterator].stop(stopMode);
            _iterator = (_iterator + 1) % NumberOfTracks;
            _musicTracks[_iterator].start();
            Paused = false;
        }

        public void RestartTrack(STOP_MODE stopMode = STOP_MODE.IMMEDIATE)
        {
            _musicTracks[_iterator].stop(stopMode);
            _musicTracks[_iterator].start();
            Paused = false;
        }

        private void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.End))
            {
                NextTrack();
            }
            if (Input.GetKeyDown(KeyCode.Home))
            {
                RestartTrack();
            }
            if (Input.GetKeyDown(KeyCode.Pause))
            {
                Paused = !Paused;
            }
        }

        private void OnDestroy()
        {
            foreach (var track in _musicTracks)
            {
                track.release();
            }
        }

        public bool Paused
        {
            get
            {
                bool rv;
                _musicTracks[_iterator].getPaused(out rv);
                return rv;
            }
            set { _musicTracks[_iterator].setPaused(value); }
        }
    }
}
