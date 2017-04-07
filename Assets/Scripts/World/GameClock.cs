using TMPro;
using System;
using UnityEngine;
using fi.tamk.hellgame.world;
using System.Collections;

public class GameClock : MonoBehaviour
{
    [SerializeField] private float BeatRadius = 0.1f;
    [SerializeField] private float Beatsize = 7.3f;
    [SerializeField] private AnimationCurve BeatCurve;

    TextMeshProUGUI _display;
    public float Time { get { return (float) _time.TotalSeconds; } }
    private TimeSpan _time;
    private float _originalSize = 0f;

    public void Init(TextMeshProUGUI display)
    {
        _display = display;
        _time = new TimeSpan();
        StartCoroutine(BeatCycle(BeatRadius));
        _originalSize = _display.fontSize;        
    }

    private void Update()
    {
        _time += TimeSpan.FromSeconds(WorldStateMachine.Instance.DeltaTime);
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        _display.SetText(
            string.Format(
                "{0:0}:{1:00}:{2:00}",
                _time.Minutes,
                _time.Seconds,
                _time.Milliseconds / 10)
            );
    }

    private IEnumerator BeatCycle(float beatRadius)
    {        
        var timer = 0f;
        while (true)
        {
            timer += WorldStateMachine.Instance.DeltaTime;

            if (timer > 1f - beatRadius)
            {
                StartCoroutine(Beat(BeatRadius));
                timer = 0f;
            }

            yield return null;
        }
    }

    private IEnumerator Beat(float beatRadius)
    {
        var timer = 0f;
        var end = beatRadius * 2f;

        while (timer < end)
        {
            timer += WorldStateMachine.Instance.DeltaTime;
            var ratio = timer / end;
            _display.fontSize = _originalSize + BeatCurve.Evaluate(ratio) * Beatsize;
            yield return null;
        }

        _display.fontSize = _originalSize;
    }
}
