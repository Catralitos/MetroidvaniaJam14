using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBuff : Buff
{
   public float timeShotBuff;

   protected override void Pickup()
   {
      PlayerEntity.Instance.Combat.IncreaseShotTimer(timeShotBuff);
      
      Destroy(gameObject);
   }
}
