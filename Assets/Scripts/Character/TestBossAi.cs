using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.character
{
    public class TestBossAi : MonoBehaviour
    {
        private Dictionary<BossStateInstanceID, BossStateInitializer> availableStates = new Dictionary<BossStateInstanceID, BossStateInitializer>();
        private ActorComponent _actorComponent;
        private BossStateInstanceID currentInstanceID;
        private float _timer = 0f;

        // Use this for initialization
        void Awake()
        {
            _actorComponent = GetComponent<ActorComponent>();
            var tmp = GetComponentsInChildren<BossStateInitializer>();

            foreach (BossStateInitializer init in tmp)
            {
                availableStates.Add(init.instanceID, init);
            }

            var initialState = availableStates[BossStateInstanceID.ShootingA];

            _timer = initialState.StateLength;
            _actorComponent.InitializeStateMachine(initialState.GetState(_actorComponent));
        }

        // Update is called once per frame
        void Update()
        {
            _timer -= Time.deltaTime;

            if (_timer <= 0)
            {
                ChangeState();
            }
        }

        void ChangeState()
        {
            
            if (currentInstanceID == BossStateInstanceID.Idle)
            {
                
                var destinationState = availableStates[BossStateInstanceID.ShootingA];
                currentInstanceID = destinationState.instanceID;
                _timer = destinationState.StateLength;
                _actorComponent.GoToState(destinationState.GetState(_actorComponent));
            } else
            {
                var destinationState = availableStates[BossStateInstanceID.Idle];
                currentInstanceID = BossStateInstanceID.Idle;
                _timer = destinationState.StateLength;
                _actorComponent.GoToState(destinationState.GetState(_actorComponent));
            }

        }
    }
}
