using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.input;
using fi.tamk.hellgame.states;
using UnityEngine;

namespace Assets.Scripts.States.Player
{
    public abstract class InputTakingState : StateAbstract
    {
        protected InputController MyInputController;

        protected InputTakingState(ActorComponent controlledHero) : base(controlledHero)
        {
            MyInputController = ControlledActor.gameObject.GetComponent<InputController>();
            if (MyInputController == null) throw new UnityException("Input handling state can't find InputController in the GameObject!");
        }
    }
}
