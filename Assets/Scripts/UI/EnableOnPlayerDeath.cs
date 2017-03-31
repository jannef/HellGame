using fi.tamk.hellgame.world;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace fi.tamk.hellgame.ui
{

    public class EnableOnPlayerDeath : MonoBehaviour
    {
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
            if (_onDeathEvent != null) _onDeathEvent.Invoke();
            gameObject.SetActive(true);
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        public void Disable()
        {
            gameObject.SetActive(false);
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }
    }
}
