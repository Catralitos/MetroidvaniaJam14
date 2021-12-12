using Buffs;
using Player;

public class PiercingBeamUpgrade : Upgrade
{
    protected override void SetUpgrade()
    {
        PlayerEntity.Instance.unlockedPiercingBeam = true;
    }
}
