using System;
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
            base.HandleInput(deltaTime);
            ControlledActor.FireGuns();
            HeroAvatar.transform.Rotate(Vector3.up, ControlledActor.Speed * Time.deltaTime);
        }

        public override InputStates StateId
        {
            get { return InputStates.EnemyTurret; }
        }
    }
}
