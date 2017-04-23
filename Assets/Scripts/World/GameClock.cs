using TMPro;
using System;
using UnityEngine;
using fi.tamk.hellgame.world;
using System.Collections;

namespace fi.tamk.hellgame.ui
{
    public class GameClock : MonoBehaviour
    {
        [SerializeField] private float BeatRadius = 0.1f;
        [SerializeField] private float Beatsize = 7.3f;
        [SerializeField] private AnimationCurve BeatCurve;

        private TextMeshProUGUI _display;
        private bool _stopped = true;
        private bool _hasBeatenThisSecond = false;

        public static string UnsetTime { get { return "--:--:--"; } }

        public float Time
        {
            get { return (float) _time.TotalSeconds; }
        }

        private TimeSpan _time;
        private float _originalSize = 0f;

        public void Init(TextMeshProUGUI display)
        {
            _display = display;
            _time = new TimeSpan();
            _originalSize = _display.fontSize;
        }

        private void Update()
        {
            if (_stopped) return;

            _time += TimeSpan.FromSeconds(WorldStateMachine.Instance.DeltaTime);
            UpdateDisplay();

            if (_time.Milliseconds > (1000 - BeatRadius * 1000) && !_hasBeatenThisSecond)
            {
                _hasBeatenThisSecond = true;
                StartCoroutine(Beat(BeatRadius));
            }
        }

        public void StartClock()
        {
            _stopped = false;
            //StartCoroutine(BeatCycle(BeatRadius));
        }

        public float StopClock()
        {
            StopAllCoroutines();
            _stopped = true;
            return Time;
        }

        private void UpdateDisplay()
        {
            _display.SetText(FormatTime(_time));
        }

        public static string FormatTime(TimeSpan timespan)
        {
            return string.Format(
                "{0:0}:{1:00}:{2:00}",
                timespan.Minutes,
                timespan.Seconds,
                timespan.Milliseconds / 10);
        }

        private IEnumerator BeatCycle(float beatRadius)
        {
            var timer = 0f;
            bool hasBeaten = true;
            while (true)
            {
                timer += WorldStateMachine.Instance.DeltaTime;

                if (timer > 1f - beatRadius && !hasBeaten)
                {
                    hasBeaten = true;
                    StartCoroutine(Beat(BeatRadius));
                }

                if (timer >= 1f)
                {
                    hasBeaten = false;
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

            _hasBeatenThisSecond = false;
            _display.fontSize = _originalSize;
        }
    }
}
