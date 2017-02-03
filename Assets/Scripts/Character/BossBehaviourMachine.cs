using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AdversiaryCollectionID {
    ThrashMobSpawner,
    AirDropper,
    SideBlasters,
    AttachedBossTurret,
}

namespace fi.tamk.hellgame.character
{
    public class BossBehaviourMachine : MonoBehaviour
    {
        private List<AdversiaryCollection> _bossStates = new List<AdversiaryCollection>();
        private List<AdversiaryCollection> _currentActiveStates = new List<AdversiaryCollection>();

        // Use this for initialization
        public BossBehaviourMachine Initialize()
        {
            var componentArray = GetComponentsInChildren<AdversiaryCollection>();

            foreach (AdversiaryCollection collection in componentArray)
            {
                _bossStates.Add(collection.Initialize());
            }

            return this;
        }

        public void ActivateState(AdversiaryCollectionID stateID)
        {
            foreach (AdversiaryCollection collection in _bossStates)
            {
                if (collection.ID == stateID)
                {
                    collection.EnableObjects();
                    _currentActiveStates.Add(collection);

                }
            }
        }

        public void ReplaceStates(AdversiaryCollectionID replacingState)
        {
            foreach (AdversiaryCollection collection in _currentActiveStates)
            {
                collection.DisableObjects();
            }

            _currentActiveStates.Clear();

            foreach (AdversiaryCollection collection in _bossStates)
            {
                if (collection.ID == replacingState)
                {
                    collection.EnableObjects();
                    _currentActiveStates.Add(collection);
                }
            }
        }

        public void StateSelfTerminated(AdversiaryCollection terminatedState)
        {
            if (_currentActiveStates.Contains(terminatedState))
            {
                _currentActiveStates.Remove(terminatedState);
            }
        }
    }
}
