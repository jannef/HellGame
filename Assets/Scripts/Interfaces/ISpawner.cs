using fi.tamk.hellgame.character;
using UnityEngine;

namespace fi.tamk.hellgame.interfaces
{
    public interface ISpawner
    {
        MinionComponent[] Spawn(ScriptableObject instructions);
    }
}
