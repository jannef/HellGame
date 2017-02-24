using fi.tamk.hellgame.effector;
using UnityEngine;
using fi.tamk.hellgame.utils;

namespace fi.tamk.hellgame.effectors
{
    public class CollectiableDropEffect : Effector
    {
        [SerializeField] protected int amountDropped;
        [SerializeField] protected float Radius;
        [SerializeField] protected float ScatterForce;
        [SerializeField] protected float Height;

        public override void Activate()
        {
            base.Activate();
            Effect.SetOnstart(LaunchCollectiables, new float[0]);
        }

        void LaunchCollectiables(float[] args)
        {
            var degrees = 360 / amountDropped; 
            for (int i = 0; i < amountDropped; i++)
            {
                var vec = Quaternion.Euler(0, degrees * i, 0) * Vector3.forward;
                var go = Pool.Instance.GetObject(Pool.PickupPrefab);
                go.transform.position = transform.position + vec * Radius + Vector3.up * Height;
                go.transform.rotation = Quaternion.Euler(0, degrees * i, 0);
                //var go = Instantiate(DropPrefab, transform.position + vec * Radius + Vector3.up * Height, Quaternion.Euler(0, degrees * i, 0));
                go.GetComponent<Rigidbody>().AddExplosionForce(ScatterForce, transform.position, Radius + 1f);
            }            
        }
    }
}
