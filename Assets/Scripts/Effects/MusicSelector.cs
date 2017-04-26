using UnityEngine;
using System.Collections;

namespace fi.tamk.hellgame.effects
{
    public class MusicSelector : MonoBehaviour
    {
        private static MusicPlayer Music
        {
            get
            {
                // ReSharper disable once ConvertIfStatementToNullCoalescingExpression
                // Unity things.
                if (_music == null)
                {
                    _music = FindObjectOfType<MusicPlayer>();
                }
                return _music;
            }
        }
        private static MusicPlayer _music;

        public void SwapToTrack(int trackZeroBaseIndex)
        {
            // Default value of -1 neatly ignores this, when called
            // automatically on room start event!
            if (trackZeroBaseIndex < 0) return;

            Music.LoopTrack = true;
            Music.PlayTrackByIndex(trackZeroBaseIndex);
        }
    }
}
