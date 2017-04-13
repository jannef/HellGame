using fi.tamk.hellgame.character;
using fi.tamk.hellgame.dataholders;
using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace fi.tamk.hellgame.states
{
    class BlobMobVersion : StateAbstract
    {

        private SlimeJumpData _longJump;
        private SlimeJumpData _shortJump;
        private int _amountHopped;

        public BlobMobVersion(ActorComponent controlledHero) : base(controlledHero)
        {
            var externalObjects = ControlledActor.gameObject.GetComponent<BossExternalObjects>();
            _longJump = GameObject.Instantiate(externalObjects.ScriptableObjects[0]) as SlimeJumpData;
            _shortJump = GameObject.Instantiate(externalObjects.ScriptableObjects[1]) as SlimeJumpData;
        }

        public override InputStates StateId
        {
            get { return InputStates.SlimeMob; }
        }

        public override void OnResumeState()
        {
            base.OnResumeState();
            _amountHopped++;
        }

        public override void HandleInput(float deltaTime)
        {
            base.HandleInput(deltaTime);

            if (_amountHopped >= 3)
            {
                _amountHopped = 0;
                ControlledActor.GoToState(new SlimeJumpingState(ControlledActor, ServiceLocator.Instance.GetNearestPlayer(ControlledActor.transform.position), _longJump, ControlledActor.transform));
            } else
            {
                ControlledActor.GoToState(new SlimeJumpingState(ControlledActor, ServiceLocator.Instance.GetNearestPlayer(ControlledActor.transform.position), _shortJump, ControlledActor.transform));
            }

        }
    }
}
