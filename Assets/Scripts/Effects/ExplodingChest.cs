using UnityEngine;
using System.Collections;

namespace fi.tamk.hellgame.effects
{
    public class ExplodingChest : MonoBehaviour
    {
        public void Awake()
        {
            foreach (var child in transform)
            {
                (child as Transform).transform.SetParent(null, true);
                var go = (child as Transform).gameObject.GetComponent<Rigidbody>();
                if (go != null) go.GetComponent<Rigidbody>().AddExplosionForce(160f, transform.position, 3f);
            }

            Destroy(gameObject);
        }
    }
}
