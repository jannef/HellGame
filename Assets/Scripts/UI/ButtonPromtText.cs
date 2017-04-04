using fi.tamk.hellgame.dataholders;
using fi.tamk.hellgame.input;
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
        [SerializeField] private InputController testController;

        // Use this for initialization
        void Awake()
        {
        }

        private void Start()
        {
            Activate(testController);
        }

        private void Activate(InputController controller)
        {
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

        private void Disable()
        {

        }
    }
}
