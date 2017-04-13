using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBossPuddleSpawner : MonoBehaviour {
    public GameObject[] puddlePrefabs;
    public Vector3 spawnedPuddleScale; 

	public void SpawnPuddle()
    {
        GameObject go = puddlePrefabs[Mathf.Clamp(Random.Range(0, puddlePrefabs.Length), 0, puddlePrefabs.Length-1)];

        var rotation = new Vector3(0, Random.value * 360, 0);
        go.transform.localScale = spawnedPuddleScale;

        Instantiate(go, transform.position, Quaternion.Euler(rotation));
    }
}
