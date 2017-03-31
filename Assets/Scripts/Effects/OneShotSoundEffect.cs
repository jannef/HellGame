using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.effects
{

    public class OneShotSoundEffect : MonoBehaviour
    {
        [EventRef]
        public String Event = "";

        public void Play()
        {
            if (String.IsNullOrEmpty(Event)) return;

            FMODUnity.RuntimeManager.PlayOneShot(Event, transform.position);
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
