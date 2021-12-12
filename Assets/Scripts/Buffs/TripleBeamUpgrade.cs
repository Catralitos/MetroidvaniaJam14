using Buffs;
using Player;

public class TripleBeamUpgrade : Upgrade
{
    protected override void SetUpgrade()
    {
        PlayerEntity.Instance.unlockedTripleBeam = true;
    }
}
