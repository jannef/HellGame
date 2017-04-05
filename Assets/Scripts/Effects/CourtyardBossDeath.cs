using fi.tamk.hellgame.character;
using fi.tamk.hellgame.states;
using System.Collections;
using System.Collections.Generic;
using fi.tamk.hellgame.world;
using UnityEngine;
using UnityEngine.Events;

namespace fi.tamk.hellgame.effects
{
    public class CourtyardBossDeath : MonoBehaviour
    {
        [SerializeField] private UnityEvent ModelVanishEvent;
        [SerializeField] private float _deathTime = 3.33f;
        [SerializeField] private AngryShakeEffect _shaker;
        private ActorComponent _actor;

        public void Awake()
        {
            _actor = GetComponent<ActorComponent>() ?? new UnityException("CourtyardBossDeath MonobBehaviour component" +
                "can't find reference to ActorComponent in GO: " + gameObject).Throw<ActorComponent>();
        }

        public void Activate()
        {
            if (_shaker != null) _shaker.Activate(_deathTime);
            _actor.GoToState(new CourtyardDeathPhase(_actor, _actor.CurrentState as CourtyardBase));
            StartCoroutine(CountdownToActualDeath(_deathTime));
        }

        private IEnumerator CountdownToActualDeath(float deathTime)
        {
            var timer = 0f;
            while (timer < deathTime)
            {
                timer += WorldStateMachine.Instance.DeltaTime;
                yield return null;
            }

            if (ModelVanishEvent != null) ModelVanishEvent.Invoke();
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
