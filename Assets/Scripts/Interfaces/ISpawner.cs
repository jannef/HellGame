﻿using fi.tamk.hellgame.character;
using fi.tamk.hellgame.dataholders;
using UnityEngine;

namespace fi.tamk.hellgame.interfaces
{
    public interface ISpawner
    {
        HealthComponent[] Spawn(SpawnerInstruction instructions);
    }
}
