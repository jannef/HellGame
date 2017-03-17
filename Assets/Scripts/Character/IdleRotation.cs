using fi.tamk.hellgame.world;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.character
{

    public class IdleRotation : MonoBehaviour
    {

        [SerializeField] private float _rotationSpeed;
        [SerializeField] private Vector3 _rotationDirection;

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(_rotationDirection.normalized * _rotationSpeed * WorldStateMachine.Instance.DeltaTime);
        }
    }
}
