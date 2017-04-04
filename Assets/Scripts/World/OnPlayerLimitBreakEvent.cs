using fi.tamk.hellgame.character;
using fi.tamk.hellgame.input;
using fi.tamk.hellgame.utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace fi.tamk.hellgame.world
{

    public class OnPlayerLimitBreakEvent : MonoBehaviour
    {
        public UnityEvent PlayerLimitBreakReady;
        public UnityEvent PlayerLimitBreakActivated;
        private bool hasActivated = false;

        private void Awake()
        {
            SceneManager.sceneLoaded += Initialize;
        }

        private void OnPowerUpGained(float percentage, int collected)
        {
            if (percentage >= 100 && !hasActivated)
            {
                hasActivated = true;
                if (PlayerLimitBreakReady != null) PlayerLimitBreakReady.Invoke();
            }
        }

        private void OnLimitBreakActivated()
        {
            if (PlayerLimitBreakActivated != null) PlayerLimitBreakActivated.Invoke();
        }

        private void LinkEvents()
        {
            var playerTransform = ServiceLocator.Instance.GetNearestPlayer(Vector3.zero);
            if (playerTransform == null)
            {
                StaticCoroutines.DoAfterDelay(0.1f, LinkEvents);
                return;
            }
            var limitBreakComponent = playerTransform.GetComponent<PlayerLimitBreak>();
            if (limitBreakComponent == null) return;
            limitBreakComponent.PowerUpGained += OnPowerUpGained;
            limitBreakComponent.LimitBreakActivation.AddListener(OnLimitBreakActivated);
        }

        private void Initialize(Scene scene, LoadSceneMode mode)
        {
            SceneManager.sceneLoaded -= Initialize;
            LinkEvents();
        }
    }
}
