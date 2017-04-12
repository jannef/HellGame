using System;
using TMPro;
using UnityEngine;

namespace fi.tamk.hellgame.ui
{
    public class ScoreWindow : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI TimeLabel;
        [SerializeField] private TextMeshProUGUI TimeField;
        [SerializeField] private TextMeshProUGUI LivesLabel;
        [SerializeField] private TextMeshProUGUI LivesField;
        [SerializeField] private TextMeshProUGUI TotalLabel;
        [SerializeField] private TextMeshProUGUI TotalField;

        private float _seconds;
        private int _hits;

        public void UpdateLabelTexts()
        {
            TimeLabel.text = LocaleStrings.UI_SCORE_COMPTIME;
            LivesLabel.text = LocaleStrings.UI_SCORE_HITSTAKEN;
            TotalLabel.text = LocaleStrings.UI_SCORE_TOTALTIME;
        }

        private void Awake()
        {
            UpdateLabelTexts();
        }

        public void SetData(GameClock clock, int hits, float penaltyPerHit = 10f)
        {
            _hits = hits;
            _seconds = clock.Time;

            TimeField.text = GameClock.FormatTime(TimeSpan.FromSeconds(_seconds));
            LivesField.text = string.Format("{0}x {1}", _hits, GameClock.FormatTime(TimeSpan.FromSeconds(penaltyPerHit)));
            TotalField.text = GameClock.FormatTime(TimeSpan.FromSeconds(_seconds + _hits * penaltyPerHit));
            UpdateLabelTexts();
        }

        public static ScoreWindow GetScoreWindowGo(Transform parent = null)
        {
            var go = (Instantiate(Resources.Load("Score"), parent) as GameObject);
            return go != null ? go.GetComponent<ScoreWindow>() : null;
        }
    }
}