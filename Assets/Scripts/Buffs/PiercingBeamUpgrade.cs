using Player;

namespace Buffs
{
    public class PiercingBeamUpgrade : Upgrade
    {
        protected override void SetUpgrade()
        {
            PlayerEntity.Instance.unlockedPiercingBeam = true;
        }
    }
}
