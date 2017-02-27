using UnityEngine;

namespace fi.tamk.hellgame.dataholders
{
    public enum SpawnPointSpread
    {
        Evenly, RandomEvenly, CompletelyRandom
    }

    public class SpawnerInstruction : ScriptableObject
    {
        public GameObject prefabToSpawn;
        public int numberOfSpawns = 1;
        public float delayBetweenSpawns = 0.1f;
        public float spawnAreaRandomness = 1f;
        public SpawnPointSpread SpawnPointSpread = SpawnPointSpread.Evenly;
        public int preferredSpawner = 0;
    }
}
