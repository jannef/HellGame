using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace fi.tamk.hellgame.ui
{

    public class UiLocalizedTextSetter : MonoBehaviour {
        [SerializeField] private LocaleStrings.StringsEnum _textToUse;

        // Use this for initialization
        void Start() {
            TextMeshProUGUI textMesh = GetComponent<TextMeshProUGUI>();

            if (textMesh != null)
            {
                textMesh.text = LocaleStrings.LocalizedStringFromEnum(_textToUse);
            }
        }
    }
}
