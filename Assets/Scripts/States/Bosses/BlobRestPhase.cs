using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace fi.tamk.hellgame.states
{
    public class BlobRestPhase : AimingEnemy
    {
        public BlobRestPhase(ActorComponent hc) : base(hc)
        {
        }

        public override InputStates StateId
        {
            get
            {
                return InputStates.BlobResting;
            }
        }

        public override bool RequestStateChange(InputStates requestedState)
        {
            if (requestedState != InputStates.BlobThird) return false;
            ControlledActor.GoToState(new BlobThirdFiringPhase(ControlledActor));
            return true;
        }

        public override void HandleInput(float deltaTime)
        {
            StateTime += Time.deltaTime;
            FaceTargetBehaviour(deltaTime);
        }
    }
}
