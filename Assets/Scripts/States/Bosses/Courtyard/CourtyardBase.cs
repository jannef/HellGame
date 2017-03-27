using System;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.states;
using fi.tamk.hellgame.utils;
using UnityEngine;

public class CourtyardBase : StateAbstract {
    protected enum ExternalLabel : int
    {
        Gun         = 0
    }

    protected Transform GunTransform = null;
    protected Transform GunTarget = null;
    protected BossExternalObjects Externals = null;
    protected float GunDistance = 1f;

    public override InputStates StateId
    {
        get
        {
            return InputStates.CourtyardBase;
        }
    }

    public CourtyardBase(ActorComponent controlledHero, CourtyardBase clonedState = null) : base(controlledHero)
    {
        if (clonedState == null)
        {
            Externals = controlledHero.gameObject.GetComponent<BossExternalObjects>();
            GunDistance = (Externals.ExistingGameObjects[(int)ExternalLabel.Gun].transform.position - controlledHero.gameObject.transform.position).magnitude;
            GunTransform = Externals.ExistingGameObjects[(int)ExternalLabel.Gun].transform;
        }
        else
        {
            GunTarget = clonedState.GunTarget;
            Externals = clonedState.Externals;
            GunDistance = clonedState.GunDistance;
            GunTransform = clonedState.GunTransform;
        }
    }

    protected void FindTarget()
    {
        GunTarget = ServiceLocator.Instance.GetNearestPlayer(ControlledActor.gameObject.transform.position);
    }

    protected void MoveGun()
    {
        if (GunTarget == null) FindTarget();

        if (GunTarget != null)
        {
            var gunPos = (GunTarget.position - ControlledActor.gameObject.transform.position).normalized * GunDistance;
            gunPos.y = GunTransform.localPosition.y;
            GunTransform.localPosition = gunPos;
            GunTransform.LookAt(new Vector3(GunTarget.position.x, GunTransform.position.y, GunTarget.position.z));
        }
    }

    public override void HandleInput(float deltaTime)
    {
        base.HandleInput(deltaTime);

        MoveGun();
        ControlledActor.FireGunByIndex(0);
    }
}
