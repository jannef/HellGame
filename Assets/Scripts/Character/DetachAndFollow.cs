using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.character
{
    public class DetachAndFollow : MonoBehaviour
    {
        [SerializeField] private bool _autoDetachOnStart = false;
        private Transform _parent;
        private Vector3 _offSet;

        private void Awake()
        {            
            if (_autoDetachOnStart) DetachFromParent();
        }

        private void Update()
        {
            if (_parent == null)
            {
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
            else
            {
                transform.position = _parent.position + _offSet;
            }
        }

        public void DetachFromParent()
        {
            _offSet = transform.localPosition;
            _parent = transform.parent;
            transform.parent = null;
        }
    }
}
