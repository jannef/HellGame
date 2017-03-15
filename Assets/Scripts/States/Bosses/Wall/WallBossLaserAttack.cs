using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.world;

namespace fi.tamk.hellgame.states
{
    class WallBossLaserAttack : WallBossAbstract
    {
        private PassiveTurret _leftEye;
        private PassiveTurret _rightEye;
        private float _movementTimer = 0;
        private WallBossLaserAttackStats _stats;
        private int attackNumber = 0;
        private bool attacking = false;
        private event Action _stopFiringLasers;

        public WallBossLaserAttack(ActorComponent controlledHero, WallBossAbstractValues values, PassiveTurret leftEye, PassiveTurret rightEye, 
            WallBossLaserAttackStats stats) : base(controlledHero, values)
        {
            _leftEye = leftEye;
            _rightEye = rightEye;
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
            if (BaseValues.currentPositionIndex == 0)
            {
                BaseValues.currentPositionIndex = 2;
            } else
            {
                BaseValues.currentPositionIndex = 0;
            }
        }

        private void Attack()
        {
            if (_stats._movements.Count > attackNumber)
            {
                ChangeCurrentWayPointIndex();

                ControlledActor.GoToState(new WallBossMove(ControlledActor, BaseValues, 
                    BaseValues.wayPointList.WayPointList[BaseValues.currentPositionIndex].position,
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

        public override void OnExitState()
        {
            base.OnExitState();
            if (_stopFiringLasers != null) _stopFiringLasers.Invoke();
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
