using Player;

namespace Buffs
{
    public class DoubleJumpUpgrade : Upgrade
    {
        protected override void SetUpgrade()
        {
            PlayerEntity.Instance.UI.DisplayTooltip(
                "You have collected the double jump upgrade. You can jump again in midair.");
            PlayerEntity.Instance.unlockedDoubleJump = true;
        }
    }
}
