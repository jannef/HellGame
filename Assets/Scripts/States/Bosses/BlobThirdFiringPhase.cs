using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.states
{
    public class BlobThirdFiringPhase : AimingEnemy {
        private float singleBeatLength = 1.4f;
        private int tempo = 0;

        public BlobThirdFiringPhase(ActorComponent hc) : base(hc)
        {
        }

        public override InputStates StateId
        {
            get
            {
                return InputStates.BlobThird;
            }
        }

        public override bool RequestStateChange(InputStates requestedState)
        {
            if (requestedState == InputStates.BlobResting)
            {
                ControlledActor.GoToState(new BlobRestPhase(ControlledActor));
                return true;
            }

            return false;
        }

        public override void HandleInput(float deltaTime)
        {
            StateTime += deltaTime;
            FaceTargetBehaviour(deltaTime);

            if (TargetTransform == null) return;
            switch (tempo)
            {
                case 3:
                    ControlledActor.FireGunByIndex(2);
                    break;
                case 4:
                    ControlledActor.FireGunByIndex(1);
                    break;
                default:
                    ControlledActor.FireGunByIndex(0);
                    break;
            }

            if (!(StateTime >= singleBeatLength)) return;
            StateTime = 0;
            tempo = (tempo + 1) % 5;
        }
    }
}
