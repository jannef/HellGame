using fi.tamk.hellgame.input;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace fi.tamk.hellgame.ui
{

    public class ShootingButtonPromt : MonoBehaviour
    {
        private TextMeshPro _promtText;
        [SerializeField] private string _gamepadText;
        [SerializeField] private string _pcText;

        // Use this for initialization
        void Start()
        {
            ChangeText(false);
            MultipleInputReader.primaryInputChanged += ChangeText;
        }

        public void ChangeText(bool isGamePad)
        {
            _promtText = GetComponent<TextMeshPro>();

            if (_promtText == null) return;

            if (isGamePad)
            {
                _promtText.text = _gamepadText;
            }
            else
            {
                _promtText.text = _pcText;
            }
        }

        private void OnDisable()
        {
            MultipleInputReader.primaryInputChanged -= ChangeText;
        }
    }
}
