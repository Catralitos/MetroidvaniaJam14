using Buffs;
using Player;

public class DashUpgrade : Upgrade
{
    protected override void SetUpgrade()
    {
        PlayerEntity.Instance.unlockedDash = true;
    }
}
