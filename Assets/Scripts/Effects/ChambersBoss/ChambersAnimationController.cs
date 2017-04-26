using UnityEngine;
using System.Collections;

namespace fi.tamk.hellgame.effects
{
    public class ChambersAnimationController : MonoBehaviour
    {
        [SerializeField] private string _fieldName;
        private Animator _animator;

        private void Awake()
        {
            _animator = gameObject.GetComponent<Animator>();
        }

        public void SetBool(bool @value)
        {
            _animator.SetBool(_fieldName, @value);
        }
    }
}
