using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.world
{

    public class BossRoomBeaten : MonoBehaviour
    {

        public void RoomHasBeenBeaten()
        {
            RoomIdentifier.OnRoomCompleted();
        }
    }
}
