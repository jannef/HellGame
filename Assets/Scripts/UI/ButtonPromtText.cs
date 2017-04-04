using fi.tamk.hellgame.dataholders;
using fi.tamk.hellgame.input;
using fi.tamk.hellgame.utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace fi.tamk.hellgame.ui
{

    public class ButtonPromtText : MonoBehaviour
    {
        [SerializeField] private Image _promtImage;
        [SerializeField] private Text _firstText;
        [SerializeField] private Text _middleText;
        [SerializeField] private string _textToShow;
        [SerializeField] private Text _finalText;
        [SerializeField] private ButtonPromtData _promtData;
        [SerializeField] private Buttons.ButtonScheme _buttonToShow;
        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0;
        }

        public void Activate()
        {
            var player = ServiceLocator.Instance.GetNearestPlayer(Vector3.zero);
            if (player == null) return;
            var controller = player.GetComponent<InputController>();
            if (controller == null) return;
            _canvasGroup.alpha = 1;
            KeyCode inputKeyCode = KeyCode.None;
            inputKeyCode = controller.ButtonToKeyCode(_buttonToShow);
            
            var promtData = _promtData.GetButtonPromtData(inputKeyCode);
            var stringList = _textToShow.Split('*');
            string addedString = "";

            if (promtData != null)
            {
                _promtImage.sprite = promtData._promtTexture;
                _promtImage.enabled = true;

                for (int i = 0; i < promtData._spaceAmount; i++)
                {
                    addedString = addedString + " ";
                } 
            } else
            {
                _promtImage.enabled = false;
                addedString = inputKeyCode.ToString();
            }

            _firstText.text = stringList[0];
            _finalText.text = stringList[1];
            _middleText.text = addedString;
        }

        public void Disable()
        {
            _canvasGroup.alpha = 0;
        }
    }
}
