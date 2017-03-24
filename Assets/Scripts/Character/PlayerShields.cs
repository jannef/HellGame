using fi.tamk.hellgame.character;
using fi.tamk.hellgame.utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.character
{

    public class PlayerShields : MonoBehaviour
    {
        private Renderer[] _shields;
        private float effectLenght;
        [SerializeField] private float blinkLenght;
        [SerializeField] private float startBlinkfreq;
        [SerializeField] private float endBlinkfreq;
        [SerializeField] private AnimationCurve blinkEasing;
        private Vector3 _minScale;
        private Vector3 _originalScale;

        // Use this for initialization
        void Awake()
        {
            var hc = GetComponentInParent<HealthComponent>();
            _originalScale = transform.localScale;
            _minScale = _originalScale / 5;
            transform.localScale = _minScale;
            if (hc == null) return;
            effectLenght = hc.InvulnerabilityLenght - 0.21f;
            hc.HitFlinchEffect.AddListener(ActivateShields);
            _shields = GetComponentsInChildren<Renderer>();
            var detachAndFollow = GetComponent<DetachAndFollow>();
            detachAndFollow.DetachFromParent();
        }

        void ActivateShields()
        {
            foreach (Renderer renderer in _shields)
            {
                renderer.enabled = true;
            }

            StartCoroutine(ShieldRoutine());
        }

        private IEnumerator ShieldRoutine()
        {
            var t = 0f;
            var startUpLenght = effectLenght - blinkLenght;

            while (t < 1)
            {
                if (t < .15)
                {
                    transform.localScale = Vector3.Lerp(_minScale, _originalScale, t / .15f);
                }
                t += world.WorldStateMachine.Instance.DeltaTime / startUpLenght;
                yield return null;
            }

            t = 0f;

            foreach (Renderer renderer in _shields)
            {
                StartCoroutine(StaticCoroutines.BlinkCoroutine(renderer, blinkLenght, startBlinkfreq, endBlinkfreq, blinkEasing, false));
            }
            yield return null;
        }
    }
}
