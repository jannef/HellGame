using System;
using UnityEngine;
using UnityEngine.Events;

namespace tamk.fi.hellgame.utils.debug
{
    public delegate bool GetInput(KeyCode mode);

    public static class Debug
    {
        public static GetInput GetInputFromInputModes(InputModes whichMode)
        {
            switch (whichMode)
            {
                case InputModes.Key:
                    return Input.GetKey;
                case InputModes.KeyDown:
                    return Input.GetKeyDown;
                case InputModes.KeyUp:
                    return Input.GetKeyUp;
                default:
                    throw new ArgumentOutOfRangeException("whichMode", whichMode, null);
            }
        }
    }

    [Serializable]
    public struct DebugKeyEventPair
    {
        [SerializeField] public UnityEvent Event;
        [SerializeField] public KeyCode Key;
        [SerializeField] public InputModes Mode;
    }

    public enum InputModes
    {
        Key = 0,
        KeyDown = 1,
        KeyUp = 2
    }

    public class DebugEventFirer : MonoBehaviour
    {
        [SerializeField] public DebugKeyEventPair[] Events;

        private void Update()
        {
            foreach (var @event in Events)
            {
                var d = Debug.GetInputFromInputModes(@event.Mode);
                if (d.Invoke(@event.Key))
                {
                    @event.Event.Invoke();
                }
            }
        }
    }
}
