using fi.tamk.hellgame.character;
using fi.tamk.hellgame.states;
using UnityEngine;

public abstract class CourtyardBase : StateAbstract {

    protected Transform GunTarget;
    protected BossExternalObjects Externals;

    public CourtyardBase(ActorComponent controlledHero) : base(controlledHero)
    {

    }

}
