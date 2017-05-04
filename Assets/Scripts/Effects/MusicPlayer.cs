using System;
using UnityEngine;
using System.Collections;
using fi.tamk.hellgame.utils;
using FMOD.Studio;
using FMODUnity;
using Random = UnityEngine.Random;

namespace fi.tamk.hellgame
{
    public class MusicPlayer : MonoBehaviour
    {
        [Serializable]
        public struct Playlist
        {
            [SerializeField] public int[] Tracks;
            [HideInInspector] public int Index;

            public int RandomizeIndex()
            {
                Index = Random.Range(0, Tracks.Length - 1);
                return Tracks[Index];
            }

            public int NextIndex()
            {
                Index = Index % Tracks.Length;
                return Tracks[Index];
            }

            public int SetIndex(int toSet)
            {
                Index = toSet % Tracks.Length;
                return Tracks[Index];
            }
        }

        public int NumberOfTracks { get { return _musicTracks.Length; } }

        [EventRef] public string[] _music;
        private EventInstance[] _musicTracks;
        public Playlist[] Playlists;

        private int _iterator = 0;

        private bool _playingPlaylist = false;
        private int _playlistIndex = -1;

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
            }
        }

        private void Update()
        {
            PLAYBACK_STATE state;
            _musicTracks[_iterator].getPlaybackState(out state);

            if (state == PLAYBACK_STATE.STOPPED)
            {
                if (_playingPlaylist)
                {
                    NextTrack(ref Playlists[_playlistIndex]);
                }
                else
                {
                    RestartTrack();
                }
            }
        }

        public void PlayTrackByIndex(int index, bool abandonPlaylist = true, STOP_MODE stopMode = STOP_MODE.ALLOWFADEOUT)
        {
            if (index < 0 || index >= NumberOfTracks)
            {
                throw new UnityException("Track index out of range!");
            }
            _playingPlaylist = !abandonPlaylist && _playingPlaylist;

            // Don't restart the track unless explicitely restarted!
            if (index == _iterator) return;

            _musicTracks[_iterator].stop(stopMode);
            _iterator = index;
            _musicTracks[_iterator].start();
            Paused = false;
        }

        public void NextTrack(ref Playlist playlist, STOP_MODE stopMode = STOP_MODE.ALLOWFADEOUT)
        {
            PlayTrackByIndex(playlist.NextIndex(), false);
            Paused = false;
        }

        public void RestartTrack(STOP_MODE stopMode = STOP_MODE.ALLOWFADEOUT)
        {
            _musicTracks[_iterator].stop(stopMode);
            _musicTracks[_iterator].start();
            Paused = false;
        }

        private void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.End))
            {
                NextTrack(ref Playlists[_playlistIndex]);
            }
            if (Input.GetKeyDown(KeyCode.Home))
            {
                RestartTrack();
            }
            if (Input.GetKeyDown(KeyCode.Pause))
            {
                Paused = !Paused;
            }
            if (Input.GetKeyDown(KeyCode.Insert))
            {
                PlayPlaylist(0);
            }
            if (Input.GetKeyDown(KeyCode.PageDown))
            {
                PlayTrackByIndex(0);
            }
            if (Input.GetKeyDown(KeyCode.PageUp))
            {
                Debug.Log(_iterator);
            }
        }

        public void PlayPlaylist(int whichList, int whichTrack = -1)
        {
            // if same list is set to play, do nothing.
            if (whichList == _playlistIndex && _playingPlaylist) return;

            if (whichList >= Playlists.Length || whichList < 0)
            {
                throw new UnityException(
                    string.Format("Playlist index out of bounds [0, {0}] -- index given: {1}", Playlists.Length, whichList));
            }
            _playlistIndex = whichList;
            _playingPlaylist = true;

            if (whichTrack > 0)
            {
                if (whichTrack >= Playlists.Length)
                {
                    throw new UnityException(
                        string.Format("Track index out of bounds [0, {0}] -- index given: {1}", Playlists[whichList].Tracks.Length, whichTrack));
                }

                PlayTrackByIndex(Playlists[whichList].SetIndex(whichTrack), false);
            }
            else
            {
                PlayTrackByIndex(Playlists[whichList].RandomizeIndex(), false);
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
