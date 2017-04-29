using FMODUnity;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace fi.tamk.hellgame.effects
{

    public class OneShotSoundEffect : MonoBehaviour
    {
        [EventRef]
        public String Event = "";

        [EventRef] public string[] EventArray;

        public void Play()
        {
            if (String.IsNullOrEmpty(Event)) return;

            RuntimeManager.PlayOneShot(Event, transform.position);
        }

        public void PlayFromArray(int index)
        {
            if (index < 0 || index >= EventArray.Length)
            {
                throw new UnityException(string.Format("Parameters out of range [0, {0}]: {1}", EventArray.Length - 1, index));
            }

            if (String.IsNullOrEmpty(EventArray[index])) return;
            RuntimeManager.PlayOneShot(EventArray[index], transform.position);
        }

        public void PlayRandomFromArray()
        {
            var index = Random.Range(0, EventArray.Length - 1);
            if (String.IsNullOrEmpty(EventArray[index])) return;
            RuntimeManager.PlayOneShot(EventArray[index], transform.position);
        }
    }
}
