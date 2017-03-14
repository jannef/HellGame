using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.world;
using fi.tamk.hellgame.utils;
using fi.tamk.hellgame.dataholders;

namespace fi.tamk.hellgame.states
{
    class WallBossBulletHell : StateAbstract
    {
        private PassiveTurret _leftEye;
        private PassiveTurret _rightEye;
        private PatrolWayPoint _wayPointList;
        private int _currentWayPoint;
        private WallBossBulletHellPhaseStats _stats;
        private float _movementTimer = 0;

        public WallBossBulletHell(ActorComponent controlledHero, PassiveTurret leftEye, PassiveTurret rightEye, PatrolWayPoint wayPointList, 
            int currentWayPoint, WallBossBulletHellPhaseStats stats) : base(controlledHero)
        {
            _leftEye = leftEye;
            _rightEye = rightEye;
            _wayPointList = wayPointList;
            _currentWayPoint = currentWayPoint;
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

        private void Move()
        {
            int positionIndex = UnityEngine.Random.Range((int)0, (int)3);

            while (positionIndex == _currentWayPoint)
            {
                positionIndex = UnityEngine.Random.Range((int)0, (int)3);
            }

            _currentWayPoint = positionIndex;
            ControlledActor.GoToState(new WallBossMove(ControlledActor, _wayPointList.WayPointList[_currentWayPoint].position,
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
