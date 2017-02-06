﻿using fi.tamk.hellgame.effector;
using fi.tamk.hellgame.character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.effectors
{
    public class CollectiableDropEffect : Effector
    {
        [SerializeField] float amountDropped;
        [SerializeField] float upwardsDegree;
        [SerializeField] GameObject _dropPrefab;

        public override void Activate()
        {
            base.Activate();
            Effect.SetOnstart(LaunchCollectiables, new float[0]);
        }

        void LaunchCollectiables(float[] args)
        {
            float turnDegrees = 360f / amountDropped;
            Vector3 originalRotation = new Vector3(1, upwardsDegree / 90, 1);

            for (int i = 0; i < amountDropped; i++)
            {
                // TODO: blow out the sun
            }

        }


    }
}
