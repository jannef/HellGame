using fi.tamk.hellgame.dataholders;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace fi.tamk.hellgame.ui
{

    public class ButtonGraphic : MonoBehaviour
    {
        [SerializeField] private ButtonPromtData _promtData;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Image _buttonImage;

        public void UpdateGraphics(KeyCode keyCode)
        {
            if (_promtData == null) return;
            ButtonPromtTextureReference promt = _promtData.GetButtonPromtData(keyCode);
            if (promt == null)
            {
                _text.text = keyCode.ToString();
                _text.enabled = true;
                _buttonImage.enabled = false;
            } else
            {
                _text.text = keyCode.ToString();
                _text.enabled = false;
                _buttonImage.enabled = true;
                _buttonImage.sprite = promt._promtTexture;
            }
        }

        public void Disappear()
        {
            _text.enabled = false;
            _buttonImage.enabled = false;
        }
    }
}
