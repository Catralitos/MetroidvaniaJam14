using Player;

namespace Buffs
{
    public class JumpBuff : Buff
    {
        public float timerBoost;

        protected override void Pickup()
        {
            PlayerEntity.Instance.Movement.IncreaseJumpTimer(timerBoost);

            Destroy(gameObject);



        }
    
    }
}
