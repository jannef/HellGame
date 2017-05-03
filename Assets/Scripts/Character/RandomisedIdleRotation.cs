using fi.tamk.hellgame.character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.world
{

    public class RandomisedIdleRotation : IdleRotation
    {
        [SerializeField] private float _randomVarianceOnMaxSpeed;
        [SerializeField] private float _singleRotationSettingLength;
        [SerializeField] private AnimationCurve _rotationSpeedCurve;
        [SerializeField] private float _startCoolDown;
        private float baseSpeed;
        private float currentMaxSpeed = 0f;
        private float timer = 0f;


        private void Start()
        {
            baseSpeed = _rotationSpeed;
            timer = _startCoolDown;
            currentMaxSpeed = baseSpeed + (Random.value * _randomVarianceOnMaxSpeed);
            baseSpeed *= -1;
            _randomVarianceOnMaxSpeed *= -1;
        }

        protected override void Update()
        {
            base.Update();
            timer += Time.deltaTime;
            if (timer >= _singleRotationSettingLength)
            {
                timer = 0f;
                currentMaxSpeed = baseSpeed + (Random.value * _randomVarianceOnMaxSpeed);
                baseSpeed *= -1;
            }

            _rotationSpeed = Mathf.Lerp(0, currentMaxSpeed, _rotationSpeedCurve.Evaluate(timer / _singleRotationSettingLength));
        }
    }
}
