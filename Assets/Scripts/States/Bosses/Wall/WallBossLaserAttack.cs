using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.world;

namespace fi.tamk.hellgame.states
{
    class WallBossLaserAttack : StateAbstract
    {
        private PassiveTurret _leftEye;
        private PassiveTurret _rightEye;
        private PatrolWayPoint _wayPointList;
        private int _currentWayPoint;
        private float _movementTimer = 0;
        private WallBossLaserAttackStats _stats;
        private int attackNumber = 0;
        private bool attacking = false;
        private event Action _stopFiringLasers;

        public WallBossLaserAttack(ActorComponent controlledHero, PassiveTurret leftEye, PassiveTurret rightEye, PatrolWayPoint wayPointList,
            int currentWayPoint, WallBossLaserAttackStats stats) : base(controlledHero)
        {
            _leftEye = leftEye;
            _rightEye = rightEye;
            _wayPointList = wayPointList;
            _currentWayPoint = currentWayPoint;
            _stats = stats;

            _leftEye.StopAiming();
            _rightEye.StopAiming();
            _stopFiringLasers += _leftEye.StartFiringLaser(1);
            _stopFiringLasers += _rightEye.StartFiringLaser(1);
        }

        public override void HandleInput(float deltaTime)
        {
            base.HandleInput(deltaTime);

            if (!attacking)
            {
                if (StateTime >= _stats.startDelay)
                {
                    Attack();
                    attacking = true;
                }
            } else
            {
                if (StateTime >= _stats.startDelay + _stats.endDelay)
                {
                    ControlledActor.ToPreviousState();
                }
            }
        }

        private void ChangeCurrentWayPointIndex()
        {
            if (_currentWayPoint == 0)
            {
                _currentWayPoint = 2;
            } else
            {
                _currentWayPoint = 0;
            }
        }

        private void Attack()
        {
            if (_stats._movements.Count > attackNumber)
            {
                ChangeCurrentWayPointIndex();

                ControlledActor.GoToState(new WallBossMove(ControlledActor, _wayPointList.WayPointList[_currentWayPoint].position,
                    _stats._movements[attackNumber]));
                
            } else
            {
                if (_stopFiringLasers != null) _stopFiringLasers.Invoke();
                return;
            }

            attackNumber++;
        }

        public override void OnResumeState()
        {
            base.OnResumeState();
            Attack();
            
        }

        public override InputStates StateId
        {
            get
            {
                return InputStates.AimingEnemy;
            }
        }
    }
}
