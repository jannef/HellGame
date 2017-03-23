using fi.tamk.hellgame.dataholders;
using fi.tamk.hellgame.ui;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPopUpTrigger : MonoBehaviour {
    private Collider _collider;
    [SerializeField] protected RoomPopUpData popUpData;
    public event RoomPopUpAction PlayerEnterEvent;
    public event Action PlayerExitTriggerEvent;
    private bool isPlayerIn = false;

    // Use this for initialization
    void Start () {
        _collider = GetComponent<Collider>();
	}
	
	void OnTriggerEnter(Collider other)
    {
        if (PlayerEnterEvent != null) PlayerEnterEvent.Invoke(popUpData);
        isPlayerIn = true;
    }

    void OnTriggerStay(Collider other)
    {
        if (!isPlayerIn)
        {
            if (PlayerEnterEvent != null) PlayerEnterEvent.Invoke(popUpData);
            isPlayerIn = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (PlayerExitTriggerEvent != null) PlayerExitTriggerEvent.Invoke();
        isPlayerIn = false;
    }
}
