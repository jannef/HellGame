using fi.tamk.hellgame.world;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnPauseTrigger : MonoBehaviour {

    public UnityEvent OnPauseEvent;
    public UnityEvent OnResumeEvent;

    void Awake()
    {
        RoomIdentifier.AddOnPauseListenerAtAwake(TriggerPause);
        RoomIdentifier.AddOnResumeListenerAtAwake(TriggerResume);
    }

    private void TriggerPause()
    {
        if (OnPauseEvent != null) OnPauseEvent.Invoke();
    }

    private void TriggerResume()
    {
        if (OnResumeEvent != null) OnResumeEvent.Invoke();
    }
}
