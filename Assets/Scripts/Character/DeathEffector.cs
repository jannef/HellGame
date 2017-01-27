using fi.tamk.hellgame.effects;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace fi.tamk.hellgame.character
{
    public class DeathEffector : MonoBehaviour
    {
        public static void ScreenShakeEffect(float[] args)
        {
            if (args.Length < 2) return;
            ScreenShaker.Instance.Shake(args[0], args[1]);
        }

        public virtual GenericEffect Die()
        {
            return GenericEffect.GetGenericEffect(transform);
        }
    }
}
