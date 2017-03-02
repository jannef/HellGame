using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.character
{
    public class DetachAndFollow : MonoBehaviour
    {
        private Transform _parent;

        private void Awake()
        {
            _parent = transform.parent;
            transform.SetParent(null);
            DontDestroyOnLoad(this.gameObject);
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
                transform.position = _parent.position;
            }
        }
    }
}
