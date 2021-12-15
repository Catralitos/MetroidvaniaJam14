using Player;

namespace Buffs
{
    public class DashUpgrade : Upgrade
    {
        protected override void SetUpgrade()
        {
            PlayerEntity.Instance.UI.DisplayTooltip(
                "You have collected the dash upgrade. You can now dash in midair or on the ground using Left-Shift.");
            PlayerEntity.Instance.unlockedDash = true;
        }
    }
}