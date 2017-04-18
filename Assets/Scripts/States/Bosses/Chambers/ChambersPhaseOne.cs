using System;
using UnityEngine;
using System.Collections;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.utils;
using fi.tamk.hellgame.world;

namespace fi.tamk.hellgame.states
{
    public class ChambersPhaseOne : ChambersBase
    {
        private readonly Transform _playerTransform;
        private float _laserCycle;

        public ChambersPhaseOne(ActorComponent controlledHero, ChambersBase clonedState = null) : base(controlledHero, clonedState)
        {
            _playerTransform = ServiceLocator.Instance.GetNearestPlayer();
            _laserCycle = NumericData.ActorFloatData[(int) FloatDataLabels.PhaseOneLaserCooldown] +
                          NumericData.ActorFloatData[(int) FloatDataLabels.PhaseOneLaserBurstDuration];
        }

        public override InputStates StateId
        {
            get { return InputStates.ChambersPhaseOne; }
        }

        public override void HandleInput(float deltaTime)
        {
            base.HandleInput(deltaTime);
            if (_playerTransform != null) NavigationAgent.SetDestination(_playerTransform.position);
            if (StateTime > _laserCycle)
            {
                StateTime = 0f;
                DefaultLaserBurst();
            }

            if (Input.GetKeyDown(KeyCode.Delete))
            {
                CogGun.BulletSystem.OneshotBehaviour(0, false, _playerTransform);
            }
        }

        public override void OnEnterState()
        {
            base.OnEnterState();
            DefaultLaserBurst();
        }
    }
}
