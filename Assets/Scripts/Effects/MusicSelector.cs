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

        private void SwapToTrack(int trackZeroBaseIndex)
        {
            Music.LoopTrack = true;
            Music.PlayTrackByIndex(trackZeroBaseIndex);
        }
    }
}
