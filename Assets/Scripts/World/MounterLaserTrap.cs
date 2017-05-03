using fi.tamk.hellgame.character;
using fi.tamk.hellgame.utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace fi.tamk.hellgame.world
{

    public class MounterLaserTrap : MonoBehaviour
    {
        public UnityEvent startLaser;
        public UnityEvent stopLaser;
        public UnityEvent deathEvent;

        [SerializeField] private float _cooldown;
        [SerializeField] private float _laserAttackLength;
        [SerializeField] private LaserEmitter _emitter;
        private Action _stopLaserAction;

        public void Stop()
        {
            if (_stopLaserAction != null) _stopLaserAction.Invoke();
            if (deathEvent != null) deathEvent.Invoke();
            StopAllCoroutines();

        }

        public void Activate()
        {
            StartCoroutine(StaticCoroutines.DoAfterDelay(_cooldown, StartAttack));
        }

        private void StartAttack()
        {

            if (startLaser != null) startLaser.Invoke();

            _stopLaserAction = _emitter.FireUntilFurtherNotice();
            StartCoroutine(StaticCoroutines.DoAfterDelay(_laserAttackLength, StopAttack));
        }

        private void StopAttack()
        {

            if (_stopLaserAction != null) _stopLaserAction.Invoke();
            if (stopLaser != null) stopLaser.Invoke();

            StartCoroutine(StaticCoroutines.DoAfterDelay(_cooldown, StartAttack));
        }
    }
}
