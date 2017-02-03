using fi.tamk.hellgame.character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.character
{
    public class AdversiaryCollection : MonoBehaviour
    {
        public AdversiaryCollectionID ID;
        [SerializeField]
        private List<GameObject> IndependentActorParents;

        public delegate void IndependentObjectEnable(object sender);
        public event IndependentObjectEnable EnabledEvent;
        public event IndependentObjectEnable DisabledEvent;

        // Use this for initialization
        public AdversiaryCollection Initialize()
        {
            foreach (GameObject go in IndependentActorParents)
            {
                var componentList = go.GetComponentsInChildren<IndependentBossGO>(true);
                foreach (IndependentBossGO independentGO in componentList)
                {
                    independentGO.AddToState(this);
                }
            }

            return this;
        }

        public void EnableObjects()
        {
            enabled = true;
            if (EnabledEvent == null) return;
            EnabledEvent.Invoke(this);
        }

        public void DisableObjects()
        {
            if (DisabledEvent == null) return;
            DisabledEvent.Invoke(this);
            enabled = false;
        }
    }
}
