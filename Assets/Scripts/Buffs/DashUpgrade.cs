using Player;

namespace Buffs
{
    public class DashUpgrade : Upgrade
    {
        protected override void SetUpgrade()
        {
            PlayerEntity.Instance.unlockedDash = true;
        }
    }
}
