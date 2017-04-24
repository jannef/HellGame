﻿using UnityEngine;
using System.Collections;
using fi.tamk.hellgame.utils;

namespace fi.tamk.hellgame.effectors
{
    public class TutorialDropEffect : CollectiableDropEffect
    {
        [SerializeField] private GameObject _tutorialPrefab;

        protected override void LaunchCollectiables(float[] args)
        {
            if (amountDropped == 0) return;
            var degrees = 360 / amountDropped;
            for (int i = 0; i < amountDropped; i++)
            {
                var vec = Quaternion.Euler(0, degrees * i, 0) * Vector3.forward;
                var go = Pool.Instance.GetObject(_tutorialPrefab);
                go.transform.position = transform.position + vec * Radius + Vector3.up * Height;
                go.transform.rotation = Quaternion.Euler(0, degrees * i, 0);
                var rigidBody = go.GetComponent<Rigidbody>();
                rigidBody.AddExplosionForce(ScatterForce + ScatterForceVariance * Random.value, transform.position, Radius + 1f);
                rigidBody.AddTorque(new Vector3(Random.value * TorqueForce, Random.value * TorqueForce, Random.value * TorqueForce));
            }
        }
    }
}
