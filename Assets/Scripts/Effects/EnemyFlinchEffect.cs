using fi.tamk.hellgame.character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.effects
{
    public class EnemyFlinchEffect : DeathEffector
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

        public override GenericEffect Activate()
        {
            StartCoroutine(ChangeColorAndBack());
            ThreadFreezeFrame(new float[0]);
            return null;
        }

        IEnumerator ChangeColorAndBack()
        {
            _renderer.material.color = _BlickColour;
            float t = 0;

            while (t < 1)
            {
                t += Time.deltaTime / blickLenght;
                yield return null;
            }

            _renderer.material.color = _startColor;

            yield return null;
        }
    }
}
