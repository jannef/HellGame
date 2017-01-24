using System.Collections;
using System.Collections.Generic;
using fi.tamk.hellgame.utils;
using fi.tamk.hellgame.utils.Stairs.Utils;
using UnityEngine;

namespace fi.tamk.hellgame.character
{
    public class BulletEmitter : MonoBehaviour
    {
        [SerializeField] protected Transform BulletOrigin;
        [SerializeField] protected GameObject[] BulletPrefabs;
        [SerializeField] protected LayerMask FireAtWhichLayer;

        protected Vector3 GunVector
        {
            get
            {
                return (BulletOrigin.position - transform.position).normalized;
            }
        }

        protected void FireBullet(Vector3 trajectory, int whichBulletIndex = 0)
        {
            if (whichBulletIndex >= BulletPrefabs.Length) return;
            var go = Pool.Instance.GetObject(BulletPrefabs[whichBulletIndex]);
            go.transform.position = BulletOrigin.position;
            go.transform.LookAt(go.transform.position + trajectory);
            go.SetLayer(FireAtWhichLayer);
        }

        protected void Awake()
        {
            FireAtWhichLayer = Utilities.GetFiringLayer((this.gameObject.layer));

            foreach (var bul in BulletPrefabs)
            {
                Pool.Instance.AddToPool(bul, 50, Camera.main.transform);
            }
        }

        public virtual void Fire()
        {
            FireBullet(GunVector);
        }
    }
}
