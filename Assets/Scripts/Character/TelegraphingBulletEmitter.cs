using fi.tamk.hellgame.character;
using fi.tamk.hellgame.world;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace fi.tamk.hellgame.character
{

    public class TelegraphingBulletEmitter : BulletEmitter
    {
        [SerializeField] protected float AnticipationLenght;
        public UnityEvent AnticipationStartEvent;
        public UnityEvent AnticipationInterruptEvent;

        public override void Fire()
        {
            if (!(Timer > Cooldown)) return;
            StartCoroutine(AnticipationCoroutine(AnticipationLenght));

            Timer = 0f;
        }

        public void InterruptAnticipation()
        {
            StopAllCoroutines();
            if (AnticipationInterruptEvent != null) AnticipationInterruptEvent.Invoke();
        }

        private void FireAfterAnticipation()
        {
            if (BurstAmount == 1)
            {
                FireBullets(GunVector);
            }
            else
            {
                StartCoroutine(FiringCoroutine(BurstAmount, TimeBetweenBursts));
            }

            if (FiringEvent != null) FiringEvent.Invoke();
        }

        private IEnumerator AnticipationCoroutine(float t)
        {
            float time = 0;
            if (AnticipationStartEvent != null) AnticipationStartEvent.Invoke();

            while (time < t)
            {
                time += WorldStateMachine.Instance.DeltaTime;
                yield return null;
            }

            FireAfterAnticipation();
        }

        private void OnDestroy()
        {
            InterruptAnticipation();
        }

    }
}
