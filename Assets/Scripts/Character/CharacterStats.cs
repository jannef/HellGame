using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.character
{
    public class CharacterStats : MonoBehaviour
    {
        public float Speed = 1;
        public float DashSpeed = 10;
        public float DashDuration = 0.75f;
        public int Health = 1;
        public float GunCooldown = 0.33f;
    }
}
