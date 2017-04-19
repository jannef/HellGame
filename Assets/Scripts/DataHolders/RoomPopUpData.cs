using fi.tamk.hellgame.world;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.dataholders
{

    public class RoomPopUpData : ScriptableObject
    {
        public LegalScenes roomIndex;
        public Sprite popUpPicture;
        public LocaleStrings.StringsEnum roomName;
    }
}
