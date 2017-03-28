using fi.tamk.hellgame.character;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.character
{

    public class PlayerRunes : MonoBehaviour
    {
        private IdleRotation _idleRotation;
        private SpriteRenderer[] _runes;
        [SerializeField] private float rotationSpeedIncreasePerHit;
        [SerializeField] private float _radius;
        private float startRotationSpeed;
        [SerializeField] private int maxActiveRuneAmount = 4;

        private void Awake()
        {
            _runes = gameObject.GetComponentFromChildrenOnly<SpriteRenderer>();
            var hc = GetComponentInParent<HealthComponent>();
            if (hc != null) hc.HealthChangeEvent += UpdateRunes;
            _idleRotation = GetComponent<IdleRotation>();
            startRotationSpeed = _idleRotation._rotationSpeed;

            var detachAndFollow = GetComponent<DetachAndFollow>();
            detachAndFollow.DetachFromParent();
            UpdateRunes(0, hc.Health, 0);
        }

        public void UpdateRunes(float percentage, int currentHp, int maxHp)
        {
            var activatedRuneAmount = 0;
            List<Transform> activeRuneTransforms = new List<Transform>();

            foreach(SpriteRenderer renderer in _runes)
            {
                if (activatedRuneAmount < currentHp)
                {
                    renderer.enabled = true;
                    activatedRuneAmount++;
                    activeRuneTransforms.Add(renderer.transform.parent);
                } else
                {
                    renderer.enabled = false;
                }
            }

            UpdateRunePositions(activeRuneTransforms);
            UpdateRotation(activatedRuneAmount);
        }

        private void UpdateRunePositions(List<Transform> transforms)
        {
            float degreeAmount = 360f / transforms.Count;

            for (int i = 0; i < transforms.Count; ++i)
            {
                transforms[i].localRotation = Quaternion.Euler(new Vector3(0, 0, degreeAmount * i));
            }
        }

        private void UpdateRotation(int runeAmount)
        {
            _idleRotation._rotationSpeed = startRotationSpeed + ((maxActiveRuneAmount - Mathf.Clamp(runeAmount, 1, maxActiveRuneAmount)) * rotationSpeedIncreasePerHit);
        }
    }
}
