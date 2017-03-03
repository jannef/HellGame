using System.Collections;
using System.Collections.Generic;
using fi.tamk.hellgame.world;
using UnityEngine.UI;
using UnityEngine;

public class FPS : MonoBehaviour {
    Text _text;
	// Use this for initialization
	void Start () {
        _text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        _text.text = (1f / WorldStateMachine.Instance.DeltaTime).ToString();
	}
}
