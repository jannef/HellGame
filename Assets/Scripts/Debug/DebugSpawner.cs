using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSpawner : MonoBehaviour {
    [SerializeField] private GameObject _spawnedObject;
    [SerializeField] private float _delayinInSpawning;
    private float timer = 0f;

    // Update is called once per frame
    public void Update () {
        timer += Time.deltaTime;

        if (timer >= _delayinInSpawning)
        {
            GameObject go = Instantiate(_spawnedObject);
            go.transform.position = transform.position;
            Destroy(this.gameObject);
        }
	}
}
