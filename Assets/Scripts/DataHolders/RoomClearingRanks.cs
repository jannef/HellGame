using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ClearingRank : int
{
    None    = -1,
    D       = 0,
    C       = 1,
    B       = 2,
    A       = 3,
    S       = 4
}

namespace fi.tamk.hellgame.dataholders
{

    public class RoomClearingRanks : ScriptableObject
    {
        [SerializeField] protected float[] Ranks;

        public ClearingRank GetRankFromTime(float time)
        {
            if (Ranks.Length != 5) throw new UnityException(string.Format("Ranks lenght is not 5, lenght is: {0}", Ranks.Length));

            for (int i = Ranks.Length - 1; i > 0; --i)
            {
                if (time < Ranks[i])
                {
                    return (ClearingRank)i;
                }
            }

            return ClearingRank.D;
        }

        public float GetNextRankTeaser(out ClearingRank nextRank, ClearingRank whichRank)
        {
            var numeric = (int) whichRank + 1;
            nextRank = numeric <= 4 ? (ClearingRank) numeric : ClearingRank.None;
            return Ranks[nextRank == ClearingRank.None ? 4 : (int)nextRank];;
        }

        public static string GetRankName(ClearingRank rank)
        {
            switch (rank)
            {                
                case ClearingRank.D:
                    return LocaleStrings.RANK_D;
                case ClearingRank.C:
                    return LocaleStrings.RANK_C;
                case ClearingRank.B:
                    return LocaleStrings.RANK_B;
                case ClearingRank.A:
                    return LocaleStrings.RANK_A;
                case ClearingRank.S:
                    return LocaleStrings.RANK_S;
                default:
                case ClearingRank.None:
                    return null;
            }
        }
    }
}
