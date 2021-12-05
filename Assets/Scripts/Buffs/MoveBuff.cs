using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBuff : Buff

{
    public float moveTimer;

    protected override void Pickup()
    {
        PlayerEntity.Instance.Movement.IncreaseMoveTimer(moveTimer);
        Destroy(gameObject);
    }
}
