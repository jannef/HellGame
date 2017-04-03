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
        private Image _promtImage;
        private Text _text;
        [SerializeField] private ButtonPromtData _promtData;
        [SerializeField] private Buttons.ButtonScheme _buttonToShow;
        [SerializeField] private InputController testController;

        // Use this for initialization
        void Awake()
        {
            _promtImage = GetComponentInChildren<Image>();
            _text = GetComponent<Text>();
        }

        private void Start()
        {
            Activate(testController);
        }

        private void Activate(InputController controller)
        {
            KeyCode inputKeyCode = KeyCode.None;
            Debug.Log(controller.GetStringReferenceToInput(_buttonToShow).Replace(" ", ""));

            try
            {
                var KeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), controller.GetStringReferenceToInput(_buttonToShow).Replace(" ",""));
            }
            catch (Exception ex)
            {
                Debug.Log("KeyCode not found");
            }

            
            var promtData = _promtData.GetButtonPromtData(inputKeyCode);
            var stringList = _text.text.Split('*');
            string addedString = "";

            var newString = new string[stringList.Length + 1];

            if (promtData != null)
            {

                for (int i = 0; i < promtData._spaceAmount; i++)
                {
                    addedString = addedString + " ";
                } 
            } else
            {
                if (inputKeyCode == KeyCode.None)
                {
                    addedString = controller.ButtonToString(_buttonToShow);
                } else {
                    addedString = inputKeyCode.ToString();
                }
            }

            for (int i = 0; i < newString.Length; i++)
            {
                if (i == 1)
                {
                    newString[i] = addedString;
                } else
                {
                    newString[i] = stringList[Mathf.Clamp(i, 0, stringList.Length - 1)];
                }
                
            }

            newString[newString.Length - 2] = addedString;

            _text.text = string.Join(" ", newString);
        }

        private void Disable()
        {

        }
    }
}
