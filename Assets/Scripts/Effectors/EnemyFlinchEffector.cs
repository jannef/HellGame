using System.Collections;
using fi.tamk.hellgame.world;
using UnityEngine;

namespace fi.tamk.hellgame.effector
{
    public class EnemyFlinchEffector : Effector
    {
        private Renderer _renderer;
        public Color _BlickColour;
        private Color _startColor;
        [SerializeField] private float blickLenght;

        void Awake()
        {
            _renderer = GetComponent<Renderer>();
            _startColor = _renderer.material.color;
        }

        public override void Activate()
        {
            StartCoroutine(ChangeColorAndBack());
        }

        IEnumerator ChangeColorAndBack()
        {
            _renderer.material.color = _BlickColour;
            float t = 0;

            while (t < 1)
            {
                t += WorldStateMachine.Instance.DeltaTime / blickLenght;
                yield return null;
            }

            _renderer.material.color = _startColor;

            yield return null;
        }
    }
}
