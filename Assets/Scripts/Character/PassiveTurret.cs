using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace fi.tamk.hellgame.character
{
    class PassiveTurret : MonoBehaviour
    {
        private BulletEmitter[] emitters;
        private List<int> gunsToFire;

        void Start()
        {
            emitters = GetComponentsInChildren<BulletEmitter>();
        }

        public void AddGunToFiringList(int gunToAdd, bool cleanOthers = false)
        {
            if (cleanOthers) gunsToFire.Clear();

            if (!gunsToFire.Contains(gunToAdd))
            {
                gunsToFire.Add(gunToAdd);
            }
        }

        public void Aim(Vector3 position)
        {
            transform.forward = Vector3.RotateTowards(transform.forward,
                    new Vector3(position.x, transform.position.y,
                    position.z) - transform.position,
                    10f, 0.0f);
        }

        void Update()
        {
            foreach (int i in gunsToFire)
            {
                emitters[i].Fire();
            }
        }
    }
}
