﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.states;
using UnityEngine;

namespace fi.tamk.hellgame.states
{
    public class EnemyTurret : StateAbstract
    {
        public EnemyTurret(HeroController controlledHero) : base(controlledHero)
        {
        }

        public override void HandleInput(float deltaTime)
        {
            _stateTime += deltaTime;

            if (_stateTime > HeroStats.GunCooldown)
            {
                ControlledCharacter.FireGuns();
                _stateTime = 0f;
            }

            HeroAvatar.transform.Rotate(Vector3.up, HeroStats.Speed * Time.deltaTime);
        }

        public override InputStates StateID
        {
            get { return InputStates.EnemyTurret; }
        }
    }
}
