using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.world;
using fi.tamk.hellgame.utils;
using fi.tamk.hellgame.dataholders;
using UnityEngine;

namespace fi.tamk.hellgame.states
{
    class WallBossBulletHell : WallBossAbstract
    {
        private PassiveTurret _leftEye;
        private PassiveTurret _rightEye;
        private WallBossBulletHellPhaseStats _stats;
        private float _movementTimer = 0;


        public WallBossBulletHell(ActorComponent controlledHero, WallBossAbstractValues values, PassiveTurret leftEye, PassiveTurret rightEye, 
            WallBossBulletHellPhaseStats stats) : base(controlledHero, values)
        {
            _leftEye = leftEye;
            _rightEye = rightEye;
            _stats = stats;
        }

        public override InputStates StateId
        {
            get
            {
                return InputStates.AimingEnemy;
            }
        }

        public override void OnEnterState()
        {
            base.OnEnterState();
            var playerTransform = ServiceLocator.Instance.GetNearestPlayer(ControlledActor.transform.position);

            _leftEye.AddGunToFiringList(true, _stats.LeftEyeTurrets);
            _leftEye.AimAtTransform(playerTransform);
            _rightEye.AimAtTransform(playerTransform);
            _rightEye.AddGunToFiringList(true, _stats.RightEyeTurret);
        }

        public override void OnExitState()
        {
            base.OnExitState();
            _leftEye.StopFiring();
            _rightEye.StopFiring();
        }

        private void Move()
        {
            int positionIndex = UnityEngine.Random.Range((int)0, (int)3);

            while (positionIndex == BaseValues.currentPositionIndex)
            {
                positionIndex = UnityEngine.Random.Range((int)0, (int)3);
            }

            BaseValues.currentPositionIndex = positionIndex;
            ControlledActor.GoToState(new WallBossMove(ControlledActor, BaseValues, BaseValues.wayPointList.WayPointList[BaseValues.currentPositionIndex].position,
               _stats.MovementStats));
        }

        public override void OnResumeState()
        {
            base.OnResumeState();
        }

        public override void HandleInput(float deltaTime)
        {
            base.HandleInput(deltaTime);

            if (StateTime >= _stats.PhaseLenght)
            {
                ControlledActor.ToPreviousState();
                return;
            }

            if (StateTime >= _movementTimer + _stats.MovementTime)
            {
                _movementTimer += _stats.MovementTime;
                Move();
            }

        }
    }
}
