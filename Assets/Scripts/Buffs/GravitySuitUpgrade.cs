using Buffs;
using Player;

public class GravitySuitUpgrade : Upgrade
{
    protected override void SetUpgrade()
    {
        PlayerEntity.Instance.unlockedGravitySuit = true;
    }
}
