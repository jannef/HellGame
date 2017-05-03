﻿using fi.tamk.hellgame.world;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.character
{

    public class IdleRotation : MonoBehaviour
    {

        [SerializeField] public float _rotationSpeed;
        [SerializeField] private Vector3 _rotationDirection;
        [SerializeField] private Space _rotationSpace = Space.Self;

        // Update is called once per frame
        protected virtual void Update()
        {
            transform.Rotate(_rotationDirection.normalized * _rotationSpeed * WorldStateMachine.Instance.DeltaTime, _rotationSpace);
        }
    }
}
