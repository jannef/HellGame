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
            _promtText = GetComponent<TextMeshPro>();

            if (_promtText == null) return;

            var JoySticks = Input.GetJoystickNames();

            if (JoySticks.Length > 0)
            {
                _promtText.text = _gamepadText;
            } else
            {
                _promtText.text = _pcText;
            }
        }
    }
}
