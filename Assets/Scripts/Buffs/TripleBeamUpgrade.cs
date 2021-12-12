using Player;

namespace Buffs
{
    public class TripleBeamUpgrade : Upgrade
    {
        protected override void SetUpgrade()
        {
            PlayerEntity.Instance.unlockedTripleBeam = true;
        }
    }
}
