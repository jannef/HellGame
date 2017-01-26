using fi.tamk.hellgame.effects;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace fi.tamk.hellgame.character
{
    class DeathEffector : MonoBehaviour
    {
        private AbstractDeathEffect[] _deathEffects;
        public float DeathLenght
        {
            private set;
            get;
        }

        void Awake()
        {
            _deathEffects = GetComponents<AbstractDeathEffect>();
            _deathEffects.OrderBy(effect => effect.StartDelay);
            DeathLenght = _deathEffects[_deathEffects.Length - 1].StartDelay;
        }

        public void Die()
        {
            StartCoroutine(DeathRoutine());
        }

        IEnumerator DeathRoutine()
        {
            float timer = 0;

            for (int i = 0; i < _deathEffects.Length;)
            {
                timer += Time.deltaTime;

                for (int index = i; index < _deathEffects.Length; index++)
                {
                    if (_deathEffects[index].StartDelay <= timer)
                    {
                        _deathEffects[index].Activate();
                        i++;
                    } else
                    {
                        break;
                    }
                }

                yield return null;
            }

            // TODO: The Destroy must come only after the final effect has happened
            Destroy(this.gameObject);

            yield return null;
        }

    }
}
