using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.dataholders
{

    public class UIButtonSoundEventReferences : ScriptableObject
    {
        [FMODUnity.EventRef]
        public String PointerMovedToThisSoundEvent = "";
        [FMODUnity.EventRef]
        public String SubmitSoundEvent = "";
        [FMODUnity.EventRef]
        public String CancelSoundEvent = "";
    }
}
