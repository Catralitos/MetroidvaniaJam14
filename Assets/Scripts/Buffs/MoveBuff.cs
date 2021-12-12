using Player;

namespace Buffs
{
    public class MoveBuff : Buff

    {
        public float moveTimer;

        protected override void Pickup()
        {
            PlayerEntity.Instance.Movement.IncreaseMoveTimer(moveTimer);
            Destroy(gameObject);
        }
    }
}
