﻿using System;
using System.Collections;
using System.Collections.Generic;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.effects;
using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.utils;
using fi.tamk.hellgame.world;
using UnityEngine;

namespace fi.tamk.hellgame.states
{
    public class HomingEnemyState : StateAbstract
    {
        private Transform _targetTransform;
        private readonly Transform _ownTransform;
        private float _retrytimer = 0f;
        private const float RetryTimeout = 1f;

        public override InputStates StateID
        {
            get { return InputStates.HomingEnemy; }
        }

        public override void OnEnterState()
        {
            _targetTransform = ServiceLocator.Instance.GetNearestPlayer(ControlledActor.transform.position);
        }

        protected override void CheckForFalling()
        {
        }

        public override void HandleInput(float deltaTime)
        {
            base.HandleInput(deltaTime);

            if (_targetTransform != null)
            {
                // TODO: Rotating speed is now determined by dashingSpeed
                _ownTransform.forward = Vector3.RotateTowards(_ownTransform.forward, _targetTransform.position - _ownTransform.position,
                    ControllerHealth.DashSpeed * Time.deltaTime, 0.0f);
                HeroAvatar.Move(_ownTransform.forward * ControllerHealth.Speed * Time.deltaTime);
            }

            _retrytimer += deltaTime;
            if (_retrytimer > RetryTimeout)
            {
                _retrytimer = 0f;
                _targetTransform = _targetTransform = ServiceLocator.Instance.GetNearestPlayer(ControlledActor.transform.position);
            }
        }

        public HomingEnemyState(ActorComponent controlledHero) : base(controlledHero)
        {
            _ownTransform = ControlledActor.transform;
        }
    }
}
