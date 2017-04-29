using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace fi.tamk.hellgame.world
{
    public class TrapSensor : MonoBehaviour
    {
        public UnityEvent OnTrapTrigger;
        [SerializeField, Range(0f, 60f)] protected float Cooldown;

        private float _timer;

        private void Update()
        {
            _timer -= WorldStateMachine.Instance.DeltaTime;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_timer <= 0f && OnTrapTrigger != null)
            {
                OnTrapTrigger.Invoke();
                _timer = Cooldown;
            }
        }
    }
}
