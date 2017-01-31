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
        public EnemyTurret(ActorComponent controlledHero) : base(controlledHero)
        {
        }

        public override void HandleInput(float deltaTime)
        {
            ControlledActor.FireGuns();
            HeroAvatar.transform.Rotate(Vector3.up, ControllerHealth.Speed * Time.deltaTime);
        }

        public override InputStates StateID
        {
            get { return InputStates.EnemyTurret; }
        }

        public override bool TakeDamage(int howMuch)
        {
            var status = base.TakeDamage(howMuch);
            if (status)
            {
                ControllerHealth.FlinchFromHit();
            }
            return status;
        }
    }
}
