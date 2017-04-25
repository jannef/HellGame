using System.Collections;
using System.Collections.Generic;
using fi.tamk.hellgame.world;
using UnityEngine.UI;
using UnityEngine;

namespace fi.tamk.hellgame.ui
{
    public class FPS : MonoBehaviour
    {
        private const int Samples = 150;
        private readonly float[] _deltaArr = new float[Samples];
        private int index = 0;

        Text _text;

        private void Start()
        {
            _text = GetComponent<Text>();
            if (!RoomManager.DebugMode) _text.enabled = false;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                _text.enabled = !_text.enabled;
            }

            _deltaArr[index] = Time.unscaledDeltaTime;
            index = (index + 1) % Samples;
            if (index % 5 == 0) _text.text = string.Format("{0:000}", CalculateFps());
        }

        private float CalculateFps()
        {
            var sum = 0f;
            for (var i = 0; i < Samples; i++)
            {
                sum += _deltaArr[i];
            }
            return Samples / sum;
        }
    }
}
