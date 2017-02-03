using fi.tamk.hellgame.states;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.character
{
    public class IndependentBossGO : MonoBehaviour {
        private AdversiaryCollection _parentCollection;

        public virtual void AddToState(AdversiaryCollection collection)
        {
            _parentCollection = collection;
            collection.EnabledEvent += Enable;
            collection.DisabledEvent += Disable;
        }

        public virtual void Enable(object sender)
        {
            if (gameObject != null) gameObject.SetActive(true);
            
        }

        public virtual void Disable(object sender)
        {
            if (gameObject != null) gameObject.SetActive(false);
        }

        void OnDestroy()
        {
            if (_parentCollection == null) return; 
            _parentCollection.DisabledEvent -= Disable;
            _parentCollection.EnabledEvent -= Enable;
        }
    }
}
