using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.states
{
    public class BlobFirstFiringState : AimingEnemy {
        private float _singleBeatLength = 1.6f;
        private int tempo = 0;

        public BlobFirstFiringState(ActorComponent hc) : base(hc)
        {
        }

        public override InputStates StateId
        {
            get
            {
                return InputStates.BlobSecond;
            }
        }

        public override bool RequestStateChange(InputStates requestedState)
        {
            if (requestedState != InputStates.BlobResting) return false;

            ControlledActor.GoToState(new BlobRestPhase(ControlledActor));
            return true;
        }

        public override void HandleInput(float deltaTime)
        {
            StateTime += deltaTime;
            FaceTargetBehaviour(deltaTime);

            if (TargetTransform == null) return;

            switch (tempo)
            {
                case 3:
                    break;
                case 4:
                    ControlledActor.FireGunByIndex(1);
                    break;
                default:
                    ControlledActor.FireGunByIndex(0);
                    break;
            }

            if (!(StateTime > _singleBeatLength)) return;

            StateTime = 0;
            tempo = (tempo + 1) % 5;
        }
    }
}
