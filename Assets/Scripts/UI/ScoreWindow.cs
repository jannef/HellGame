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
        [SerializeField] private float _textFadeDuration = 0.66f;
        [SerializeField] private float _tickDuration = 0.2f;
        [SerializeField] private float _boxFadeDuration = 0.31f;
        [SerializeField] private AnimationCurve _fadeCurve;
        [SerializeField] private TextMeshProUGUI TimeLabel;
        [SerializeField] private TextMeshProUGUI TimeField;
        [SerializeField] private TextMeshProUGUI LivesLabel;
        [SerializeField] private TextMeshProUGUI LivesField;
        [SerializeField] private TextMeshProUGUI TotalLabel;
        [SerializeField] private TextMeshProUGUI TotalField;
        [SerializeField] private TextMeshProUGUI TeaserField;
        [SerializeField] private Image MedalImage;
        [SerializeField] private Image Background;
        [SerializeField] private Sprite[] Medals;

        private TextMeshProUGUI[] _allTexts;

        public void UpdateLabelTexts()
        {
            TimeLabel.text = LocaleStrings.UI_SCORE_COMPTIME;
            LivesLabel.text = LocaleStrings.UI_SCORE_HITSTAKEN;
            TotalLabel.text = LocaleStrings.UI_SCORE_TOTALTIME;
        }

        private void Awake()
        {
            _allTexts = new[] { TimeField, TimeLabel, LivesLabel, LivesField, TotalLabel, TotalField, TeaserField };

            if (Medals.Length != 5)
            {
                throw new UnityException(
                    "ScoreWindows component in the score prefab is misconfigured! Medals array must have 5 elements!");
            }

            if (Medals.Any(sprite => sprite == null))
            {
                throw new UnityException("ScoreWindow component has null value in Medals array!");
            }

            RoomIdentifier.GamePaused += SetDeactive;
            RoomIdentifier.GameResumed += SetActive;

            UpdateLabelTexts();
        }

        private void SetActive()
        {
            gameObject.transform.localScale = Vector3.one;
        }

        private void SetDeactive()
        {
            gameObject.transform.localScale = Vector3.zero;
        }

        public void FadeIn(float duration, params MaskableGraphic[] element)
        {
            StartCoroutine(Fade(false, duration, element));
        }

        public void FadeOut(float duration, params MaskableGraphic[] element)
        {
            StartCoroutine(Fade(true, duration, element));
        }

        private IEnumerator Fade(bool toTransparent, float duration, params MaskableGraphic[] element)
        {
            if (duration <= 0.1f) duration = 0.1f;

            var timer = 0f;
            while (timer < duration)
            {
                timer += WorldStateMachine.Instance.DeltaTime;
                var ratio = timer / duration;

                foreach (var e in element)
                {
                    e.color = SwapAlpha(e.color, _fadeCurve.Evaluate(ratio), 0f, 1f, toTransparent);
                }

                yield return null;
            }
        }

        private Color SwapAlpha(Color color, float ratio, float value1, float value2, bool toTransparent = false)
        {
            var alfa = !toTransparent ? Mathf.Lerp(value1, value2, ratio) : Mathf.Lerp(value2, value1, ratio);
            color.a = alfa;
            return color;
        }

        public void OnDestroy()
        {
            RoomIdentifier.GamePaused -= SetDeactive;
            RoomIdentifier.GameResumed -= SetActive;
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
            go.transform.SetAsFirstSibling();
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
            BatchSetActive(true, TimeLabel, TimeField);
            FadeIn(_boxFadeDuration, Background);
            FadeIn(_textFadeDuration, TimeLabel, TimeField); 

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
            BatchSetActive(true, LivesField, LivesLabel);
            FadeIn(_textFadeDuration, LivesField, LivesLabel);

            var hit = 0;
            while (true)
            {
                timer = 0f;
                LivesField.text = string.Format("{0}x {1}", hit, GameClock.FormatTime(TimeSpan.FromSeconds(hit * penalty)));           
                while (timer < _tickDuration)
                {
                    timer += WorldStateMachine.Instance.DeltaTime;                    
                    yield return null;
                }
                if (hit >= hits) break;
                hit++;
            }

            // Display total time
            BatchSetActive(true, TotalField, TotalLabel);
            FadeIn(_textFadeDuration, TotalField, TotalLabel);

            var totalTime = clock.Time + hits * penalty;
            var rnk = ranks.GetRankFromTime(totalTime);

            if (rnk != ClearingRank.None)
            {
                MedalImage.gameObject.SetActive(true);
                FadeIn(_textFadeDuration, MedalImage);
                MedalImage.sprite = Medals[(int)rnk];

                if (rnk != ClearingRank.S)
                {
                    ClearingRank next;
                    TeaserField.text = string.Format(LocaleStrings.UI_SCORE_TEASER, GameClock.FormatTime(TimeSpan.FromSeconds(ranks.GetNextRankTeaser(out next, rnk))));
                    BatchSetActive(true, TeaserField);
                    FadeIn(_textFadeDuration, TeaserField);
                }
            }
        }
    }
}