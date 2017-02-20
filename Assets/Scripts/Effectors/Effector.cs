using fi.tamk.hellgame.effects;
using fi.tamk.hellgame.world;
using UnityEngine;

namespace fi.tamk.hellgame.effector
{
    public class Effector : MonoBehaviour
    {
        protected GenericEffect Effect;
        protected static float _lastFreeze = 0f;

        public static void ScreenShakeEffect(float[] args)
        {
            if (args.Length < 2) return;
            ScreenShaker.Instance.Shake(args[0], args[1]);
        }

        public static void FreezeFrame(float[] args)
        {
            WorldStateMachine.Instance.FreezeFrame();
        }

        public static void ThreadFreezeFrame(float[] args)
        {
            if (Time.time - _lastFreeze < 0.1f) return;

            _lastFreeze = Time.time;
            System.Threading.Thread.Sleep(20);
        }

        public static void SlowDown(float[] args)
        {
            if (args.Length >= 2) WorldStateMachine.Instance.SlowDownPeriod(args[0], args[1]);
        }

        public virtual void Activate()
        {
            Effect = GenericEffect.GetGenericEffect(transform);
        }
    }
}
