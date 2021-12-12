using Buffs;
using Player;

public class DoubleJumpUpgrade : Upgrade
{
    protected override void SetUpgrade()
    {
        PlayerEntity.Instance.unlockedDoubleJump = true;
    }
}
