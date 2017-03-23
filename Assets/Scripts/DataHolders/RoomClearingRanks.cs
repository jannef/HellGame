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
        public float[] Ranks;


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

            return ClearingRank.None;
        }
    }
}
