using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBuff : Buff
{
    public float timerBoost;

    protected override void Pickup()
    {
        PlayerEntity.Instance.Movement.IncreaseJumpTimer(timerBoost);

        Destroy(gameObject);



    }
    
}
