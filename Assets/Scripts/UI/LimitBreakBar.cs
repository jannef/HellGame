using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.utils;
using UnityEngine;
using UnityEngine.UI;

namespace fi.tamk.hellgame.ui
{
    class LimitBreakBar : ProgressBar
    {
        [SerializeField] private PlayerLimitBreak _whichToTrackInitially; // will probably be removed!
        [SerializeField] private float _shakeIntensity; // will probably be removed!
        private Text _availableText;
        private Coroutine shakeRoutine;
        private Vector2 _startingPositionBeforeShake;
        private RectTransform _transform;

        protected override void Awake()
        {
            base.Awake();
            _availableText = GetComponentInChildren<Text>();
            _availableText.enabled = false;
            AttachToLimitBreak(_whichToTrackInitially);
            _transform = GetComponent<RectTransform>();
            _startingPositionBeforeShake = _transform.anchoredPosition;
        }

        private void ChangeBar(int currentPoints, int maxPoints)
        {
            SetBarProgress((float)currentPoints / (float) maxPoints, 0);

            if (currentPoints >= maxPoints)
            {
                IndicateAvailableLimitBreak();
            }
        }

        private void LimitBreakActivated()
        {
            _availableText.enabled = false;
            StartCoroutine(DrainRoutine(_whichToTrackInitially._modifiableStats.LimitBreakLenght));
        }

        public void AttachToLimitBreak(PlayerLimitBreak toWhich)
        {
            toWhich.PowerUpGained += ChangeBar;
            toWhich.LimitBreakActivation.AddListener(LimitBreakActivated);
            ChangeBar(0, 100);
        }

        private void IndicateAvailableLimitBreak()
        {
            _availableText.enabled = true;
            shakeRoutine = StartCoroutine(StaticCoroutines.ConstantUIShakeRoutine(GetComponent<RectTransform>(), _shakeIntensity));
        }

        private IEnumerator DrainRoutine(float lenght)
        {
            float t = 0;

            while (t <= 1f)
            {
                t += Time.deltaTime / lenght;
                SetBarProgress(0, Mathf.Lerp(1, 0, t));
                yield return null;
            }

            SetBarProgress(0, 0);
            if (shakeRoutine != null) StopCoroutine(shakeRoutine);
            _transform.anchoredPosition = _startingPositionBeforeShake;


            yield return null;
        }
    }
}
