using fi.tamk.hellgame.world;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.effects
{
    public class AngryShakeEffect : MonoBehaviour
    {
        [Range(0.01f, 10f), SerializeField] private float _duration = 5f;
        [Range(0.01f, 10f), SerializeField] private float _amounth = 0.2f;

        public void Activate()
        {

        }

        private IEnumerator ShakeDat(float duration)
        {
            Vector3 original = transform.position;
            var timer = 0f;

            while (timer < duration)
            {
                var offset = Random.insideUnitSphere * _amounth;
                offset.y = 0;

                timer += WorldStateMachine.Instance.DeltaTime;
                yield return null;
            }

            transform.position = original;
        }
    }
}
