﻿using fi.tamk.hellgame.dataholders;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace fi.tamk.hellgame.ui
{

    public class ValueSlider : MonoBehaviour
    {
        private enum SettingValueToChange
        {
            SFXVolume, MusicVolume
        }

        private Slider _attachedSlider;

        [SerializeField] private SettingValueToChange _settingValueToChange;

        private void Start()
        {
            _attachedSlider = GetComponent<Slider>();
            float value = 1;
            var settings = UserStaticData.GetGameSettings();

            switch (_settingValueToChange)
            {
                case SettingValueToChange.SFXVolume:
                    value = settings.SFXVolume;
                    break;
                case SettingValueToChange.MusicVolume:
                    value = settings.MusicVolume;
                    break;
            }

            _attachedSlider.value = value;
            _attachedSlider.onValueChanged.AddListener(OnValueChanged);
        }

        public void OnValueChanged(float newValue)
        {

            switch (_settingValueToChange)
            {
                case SettingValueToChange.SFXVolume:
                    UserStaticData.SetSFXMixerVolume(newValue);
                    break;
                case SettingValueToChange.MusicVolume:
                    UserStaticData.SetMusicMixerVolume(newValue);
                    break;
            }
        }

    }
}
