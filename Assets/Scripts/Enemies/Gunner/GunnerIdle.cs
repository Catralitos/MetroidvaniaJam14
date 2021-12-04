using UnityEngine;

public class GunnerIdle : GunnerState
{
    private float _cooldownLeft;

    public static GunnerIdle Create(Gunner target)
    {
        GunnerIdle state = GunnerState.Create<GunnerIdle>(target);
        return state;
    }

    public override void StateStart()
    {
        base.StateStart();
        _cooldownLeft = target.holdPositionTime;
        target.rb.velocity = Vector2.zero;
        //animator.SetBool("Stopped", true);
        //animator.SetBool("Patrolling", false);
        //animator.SetBool("Chasing", false);
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        if (target.CheckForPlayer())
        {
            SetState(GunnerChase.Create(target));
        }

        _cooldownLeft -= Time.deltaTime;
        if (_cooldownLeft <= 0)
        {
            SetState(GunnerPatrol.Create(target));
        }
    }
}
