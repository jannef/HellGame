using fi.tamk.hellgame.utils;
using fi.tamk.hellgame.world;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace fi.tamk.hellgame.ui
{

    public class LocaleChanger : MonoBehaviour
    {
        [SerializeField] private SystemLanguage _localeToChangeTo;

        // Update is called once per frame
        public void ChangeLocale()
        {
            LocaleStrings.SetCurrentLocale(Utilities.ReturnLocaleStringBasedOnLanguageEnum(_localeToChangeTo));
            RoomManager.LoadRoom((LegalScenes)SceneManager.GetActiveScene().buildIndex, true);
        }
    }
}

