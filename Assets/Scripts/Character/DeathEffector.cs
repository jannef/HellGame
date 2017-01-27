using fi.tamk.hellgame.effects;
using fi.tamk.hellgame.world;
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

        public static void FreezeFrame(float[] args)
        {
            WorldStateMachine.Instance.FreezeFrame();
        }

        public static void SlowDown(float[] args)
        {
            if (args.Length >= 2) WorldStateMachine.Instance.SlowDownPeriod(args[0], args[1]);
        }

        public virtual GenericEffect Die()
        {
            return GenericEffect.GetGenericEffect(transform);
        }
    }
}
