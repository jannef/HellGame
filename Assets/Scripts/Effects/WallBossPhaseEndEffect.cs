using fi.tamk.hellgame.effectors;
using fi.tamk.hellgame.utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.effectors
{

    public class WallBossPhaseEndEffect : CollectiableDropEffect
    {

        protected override void LaunchCollectiables(float[] args)
        {
            if (amountDropped == 0) return;
            float degrees = 100f / (float) amountDropped;
            for (int i = 0; i < amountDropped; i++)
            {
                var vec = Quaternion.Euler(0, degrees * i - (degrees * ((float) amountDropped/2)), 0) * transform.forward;
                var go = Pool.Instance.GetObject(Pool.PickupPrefab);
                go.transform.position = transform.position + vec * Radius;
                go.transform.rotation = Quaternion.Euler(0, degrees * i, 0);
                //var go = Instantiate(DropPrefab, transform.position + vec * Radius + Vector3.up * Height, Quaternion.Euler(0, degrees * i, 0));
                var rigidBody = go.GetComponent<Rigidbody>();
                rigidBody.AddExplosionForce(ScatterForce + ScatterForceVariance * Random.value, transform.position, Radius + 1f);
                rigidBody.AddTorque(new Vector3(Random.value * TorqueForce, Random.value * TorqueForce, Random.value * TorqueForce));

            }
        }
    }

}
