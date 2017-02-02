using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.states;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossStateInstanceID
{
    Idle,
    ShootingA,
    ShootingB,

}

namespace fi.tamk.hellgame.character
{
    public class BossStateInitializer : MonoBehaviour {
        [SerializeField] private List<GameObject> IndependentActorParents;
        public BossStateInstanceID instanceID;
        [SerializeField] private InputStates stateID;
        public float StateLength;

        protected StateBossRoutine StoredBossState
        {
            get;
            private set;
        }

        public StateBossRoutine GetState(ActorComponent requester)
        {
            if (StoredBossState == null) Initialize(requester);
                return StoredBossState;
        }

        protected virtual void Initialize(ActorComponent requester)
        {

            IInputState tmp;
            StateInitializer.StateFromStateId(stateID, out tmp, requester);
            StoredBossState = (StateBossRoutine) tmp;

            foreach (GameObject go in IndependentActorParents)
            {
                var componentList = go.GetComponentsInChildren<IndependentBossGO>(true);
                foreach (IndependentBossGO independentGO in componentList)
                {
                    independentGO.AddToState(StoredBossState);
                }
            }
        }
    }
}
