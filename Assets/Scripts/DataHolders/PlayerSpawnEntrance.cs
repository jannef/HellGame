using fi.tamk.hellgame.world;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.dataholders
{
    public class PlayerSpawnEntrance : MonoBehaviour {
        [SerializeField] public LegalScenes previousScene;
        [SerializeField] public Transform StartPoint;
    }
}
