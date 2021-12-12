using Player;

namespace Buffs
{
    public class GravitySuitUpgrade : Upgrade
    {
        protected override void SetUpgrade()
        {
            PlayerEntity.Instance.unlockedGravitySuit = true;
        }
    }
}
