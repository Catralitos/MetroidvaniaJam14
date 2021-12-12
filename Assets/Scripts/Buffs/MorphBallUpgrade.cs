using Buffs;
using Player;

public class MorphBallUpgrade : Upgrade
{
    protected override void SetUpgrade()
    {
        PlayerEntity.Instance.unlockedMorphBall = true;
    }
}   
