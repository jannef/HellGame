using fi.tamk.hellgame.utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallEye : MonoBehaviour {
    private Transform _playerTransform;
    [SerializeField] float _maxRotation = 45f;
    private Vector3 _originalForward;

    private void Start()
    {
        _originalForward = transform.up;
        _playerTransform = ServiceLocator.Instance.GetNearestPlayer(Vector3.zero);
    }

    private void Update()
    {
        if (_playerTransform != null)
        {
            var lookRotation = (_playerTransform.position - transform.position);
            if (Vector3.Angle(_originalForward, lookRotation) <= _maxRotation) {
                transform.up = lookRotation;
            }
        } else
        {
            _playerTransform = ServiceLocator.Instance.GetNearestPlayer(Vector3.zero);
        }
    }
}
