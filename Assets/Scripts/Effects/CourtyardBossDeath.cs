using fi.tamk.hellgame.character;
using fi.tamk.hellgame.states;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.effects
{
    public class CourtyardBossDeath : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particles;
        private ActorComponent _actor;

        public void Awake()
        {
            _actor = GetComponent<ActorComponent>() ?? new UnityException("CourtyardBossDeath MonobBehaviour component" +
                "can't find reference to ActorComponent in GO: " + gameObject).Throw<ActorComponent>();
        }

        public void Activate()
        {
            if (_particles != null) _particles.Play();
            _actor.GoToState(new CourtyardDeathPhase(_actor, _actor.CurrentState as CourtyardBase));
        }
    }
}
