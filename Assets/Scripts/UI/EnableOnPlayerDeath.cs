using fi.tamk.hellgame.world;
using System.Collections;
using System.Collections.Generic;
using fi.tamk.hellgame.utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace fi.tamk.hellgame.ui
{

    public class EnableOnPlayerDeath : MonoBehaviour
    {
        public float Delay = 1f;
        private CanvasGroup _canvasGroup;
        public UnityEvent _onDeathEvent;

        // Use this for initialization
        void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            SceneManager.sceneLoaded += Initialize;
            gameObject.SetActive(false);
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }

        private void Initialize(Scene loadedScene, LoadSceneMode mode)
        {
            SceneManager.sceneLoaded -= Initialize;
            RoomIdentifier.PlayerDeath += Activate;
        }

        public void Activate()
        {
            gameObject.SetActive(true);
            _canvasGroup.alpha = 0;
            StartCoroutine(DelayedActivate());
        }

        public void Disable()
        {
            gameObject.SetActive(false);
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }

        protected IEnumerator DelayedActivate()
        {
            yield return new WaitForSecondsRealtime(Delay);
            if (_onDeathEvent != null) _onDeathEvent.Invoke();
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }
    }
}
