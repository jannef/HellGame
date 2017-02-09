using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerInstruction : ScriptableObject {

    public GameObject prefabToSpawn;
    public int numberOfSpawns = 1;
    public int[] possibleSpawnPoints;
    public float delayBetweenSpawns = 0.1f;
    public float spawnAreaRandomness = 1f;
}
