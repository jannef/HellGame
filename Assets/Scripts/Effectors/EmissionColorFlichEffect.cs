using System.Collections;
using fi.tamk.hellgame.world;
using UnityEngine;

namespace fi.tamk.hellgame.effector
{
    public class EmissionColorFlichEffect : Effector
    {
        private Material _materialToChange;
        public Color _BlickColour;
        private Color _startColor;
        [SerializeField] private float blickLenght;

        void Awake()
        {
            var renderer = GetComponent<Renderer>();
            _materialToChange = renderer.material;
            _startColor = _materialToChange.GetColor("_EmissionColor");
        }

        public override void Activate()
        {
            StartCoroutine(ChangeColorAndBack());
        }

        IEnumerator ChangeColorAndBack()
        {
            _materialToChange.SetColor("_EmissionColor", _BlickColour);
            float t = 0;

            while (t < 1)
            {
                t += WorldStateMachine.Instance.DeltaTime / blickLenght;
                yield return null;
            }

            _materialToChange.SetColor("_EmissionColor", _startColor);

            yield return null;
        }
    }
}
