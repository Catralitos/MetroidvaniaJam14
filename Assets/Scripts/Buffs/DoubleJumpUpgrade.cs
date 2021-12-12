using Player;

namespace Buffs
{
    public class DoubleJumpUpgrade : Upgrade
    {
        protected override void SetUpgrade()
        {
            PlayerEntity.Instance.unlockedDoubleJump = true;
        }
    }
}
