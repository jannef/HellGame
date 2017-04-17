using fi.tamk.hellgame.world;
using System;
using System.Collections;
using System.Linq;
using fi.tamk.hellgame.dataholders;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        [SerializeField] private TextMeshProUGUI TeaserField;
        [SerializeField] private Image MedalImage;
        [SerializeField] private Sprite[] Medals;

        public void UpdateLabelTexts()
        {
            TimeLabel.text = LocaleStrings.UI_SCORE_COMPTIME;
            LivesLabel.text = LocaleStrings.UI_SCORE_HITSTAKEN;
            TotalLabel.text = LocaleStrings.UI_SCORE_TOTALTIME;
        }

        private void Awake()
        {
            if (Medals.Length != 5)
            {
                throw new UnityException(
                    "ScoreWindows component in the score prefab is misconfigured! Medals array must have 5 elements!");
            }

            if (Medals.Any(sprite => sprite == null))
            {
                throw new UnityException("ScoreWindow component has null value in Medals array!");
            }

            UpdateLabelTexts();
        }

        public void SetData(GameClock clock, int hits, RoomClearingRanks ranks, float penaltyPerHit = 10f)
        {
            TotalField.text = GameClock.FormatTime(TimeSpan.FromSeconds(clock.Time + hits * penaltyPerHit));
            UpdateLabelTexts();
            StartCoroutine(Animated(2f, clock, hits, penaltyPerHit, ranks));
        }

        public static ScoreWindow GetScoreWindowGo(Transform parent = null)
        {
            var go = (Instantiate(Resources.Load("Score"), parent) as GameObject);
            return go != null ? go.GetComponent<ScoreWindow>() : null;
        }

        public static void BatchSetActive(bool toWhichState, params TextMeshProUGUI[] gos)
        {
            foreach(var go in gos)
            {
                go.gameObject.SetActive(toWhichState);
            }
        }

        private IEnumerator Animated(float duration, GameClock clock, int hits, float penalty, RoomClearingRanks ranks)
        {
            BatchSetActive(true, TimeLabel, LivesLabel, TotalLabel, TimeField);

            // Fill the used time.
            var timer = 0f;
            while (timer < duration)
            {
                timer += WorldStateMachine.Instance.DeltaTime;
                var secs = (timer / duration) * clock.Time;                
                

                TimeField.text = GameClock.FormatTime(TimeSpan.FromSeconds(secs));
                yield return null;
            }

            // Fill the hits taken.
            BatchSetActive(true, LivesField);
            timer = 0f;
            while (timer < duration)
            {
                timer += WorldStateMachine.Instance.DeltaTime;
                var hit = (int)((timer / duration) * hits); 
                

                LivesField.text = string.Format("{0}x {1}", hit, GameClock.FormatTime(TimeSpan.FromSeconds(hit * penalty)));
                yield return null;
            }

            // Display total time
            BatchSetActive(true, TotalField);

            var totalTime = clock.Time + hits * penalty;
            var rnk = ranks.GetRankFromTime(totalTime);

            if (rnk != ClearingRank.None)
            {
                MedalImage.gameObject.SetActive(true);
                MedalImage.sprite = Medals[(int)rnk];

                if (rnk != ClearingRank.S)
                {
                    ClearingRank next;
                    TeaserField.text = string.Format(LocaleStrings.UI_SCORE_TEASER, GameClock.FormatTime(TimeSpan.FromSeconds(ranks.GetNextRankTeaser(out next, rnk))));
                    BatchSetActive(true, TeaserField);
                }
            }
        }
    }
}