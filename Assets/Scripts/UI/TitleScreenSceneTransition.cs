using fi.tamk.hellgame.dataholders;
using fi.tamk.hellgame.world;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace fi.tamk.hellgame.ui
{

    public class TitleScreenSceneTransition : SceneTransitionEffect
    {
        [SerializeField] private CanvasGroup _autosaveCanvas;
        [SerializeField] private float _autosaveCanvasFadeInLenght;
        [SerializeField] private AnimationCurve _autosaveFadeInCurve;
        [SerializeField] private float _autosaveReminderLenght;
        [SerializeField] private TextMeshProUGUI _autosaveReminderText;
        [SerializeField] private float _autosaveReminderTextFadeInLenght;
        [SerializeField] private AnimationCurve _autosaveReminderTextCurve;

        public override void StartSceneTransition(int sceneToLoad)
        {
            var SaveGame = UserStaticData.GetRoomData((int) LegalScenes.Cellar_1);

            if (SaveGame == null && sceneToLoad != SceneManager.GetActiveScene().buildIndex)
            {
                StartCoroutine(AutoSaveReminderRoutine(sceneToLoad));
                return;
            }

            base.StartSceneTransition(sceneToLoad);
        }

        IEnumerator AutoSaveReminderRoutine(int sceneToLoad)
        {
            _autosaveReminderText.alpha = 0;
            var t = 0f;
            while (t < 1)
            {
                t += Time.unscaledDeltaTime / _autosaveCanvasFadeInLenght;
                _autosaveCanvas.alpha = _autosaveFadeInCurve.Evaluate(t);

                yield return null;
            }

            _autosaveCanvas.alpha = _autosaveFadeInCurve.Evaluate(1);

            t = 0f;
            while (t < 1)
            {
                t += Time.unscaledDeltaTime / _autosaveReminderTextFadeInLenght;
                _autosaveReminderText.alpha = _autosaveFadeInCurve.Evaluate(t);
                yield return null;
            }

            _autosaveReminderText.alpha = _autosaveFadeInCurve.Evaluate(1);

            t = 0f;
            while (t < 1)
            {
                t += Time.unscaledDeltaTime / _autosaveReminderLenght;
                if (Input.anyKeyDown)
                {
                    break;
                }
                yield return null;
            }

            base.StartSceneTransition(sceneToLoad);
        }
    }
}
