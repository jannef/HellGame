using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.ui
{

    public class RankSprites : ScriptableObject
    {
        public Sprite[] _rankSprites;
        public Sprite _noRankSprite;

        public Sprite ReturnSpriteByRank(ClearingRank rank)
        {
            Sprite returnSprite = null;

            if (rank == ClearingRank.None)
            {
                return _noRankSprite;
            }

            if (rank != ClearingRank.None)
            {
                if (rank < 0 || (int) rank >= _rankSprites.Length)
                {
                    return null;
                }

                returnSprite = _rankSprites[(int)rank];
            }

            return returnSprite;
        }
    }
}
