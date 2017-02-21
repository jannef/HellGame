using UnityEngine;
using UnityEngine.UI;

namespace fi.tamk.hellgame.ui
{
    public class ProgressBar : MonoBehaviour
    {
        private Material _bar = null;
        private Image _barImage = null;

        protected virtual void Awake()
        {
            _barImage = GetComponent<Image>();
            _bar = Instantiate(_barImage.material);
            _barImage.material = _bar;
            SetBarProgress(1f);
        }

        protected void SetBarProgress(float progress, float segmentTwoProgress = 0f)
        {
            _bar.SetFloat("_Progress", progress);
            _bar.SetFloat("_SegmentTwo", segmentTwoProgress);
        }
    }
}
