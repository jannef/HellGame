using System.Collections;
using System.Collections.Generic;
using System.Linq;
using fi.tamk.hellgame.world;
using UnityEngine;

public class SlimeBossPuddleSpawner : MonoBehaviour {
    public GameObject[] puddlePrefabs;
    public Vector3 spawnedPuddleScale;
    public float GrowInsteadSpawnDistance = 3f;

    public static List<SlimeBossPuddle> Puddles;

    private void Awake()
    {
        Puddles = new List<SlimeBossPuddle>();
    }

	public void SpawnPuddle()
    {
        // Refresh closeby puddle instead of spawning a new one
        if (Puddles.Count > 0)
        {
            var closest = Puddles.OrderBy(x => (x.transform.position - transform.position).sqrMagnitude).First();
            if ((closest.transform.position - transform.position).magnitude < GrowInsteadSpawnDistance)
            {
                closest.RefreshPuddle(0.3f, 0.8f);
                return;
            }
        }

        GameObject go = Instantiate(puddlePrefabs[Mathf.Clamp(Random.Range(0, puddlePrefabs.Length), 0, puddlePrefabs.Length-1)],
            transform.position,
            Quaternion.Euler(new Vector3(0, Random.value * 360, 0)));

        var puddle = go.GetComponent<SlimeBossPuddle>();
        puddle.InitializePuddle();
        puddle.SetLocalScale(spawnedPuddleScale);
        Puddles.Add(puddle);
    }
}
