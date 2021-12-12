using Player;

namespace Buffs
{
   public class DamageBuff : Buff
   {
      public float timeShotBuff;

      protected override void Pickup()
      {
         PlayerEntity.Instance.Combat.IncreaseShotTimer(timeShotBuff);
      
         Destroy(gameObject);
      }
   }
}
