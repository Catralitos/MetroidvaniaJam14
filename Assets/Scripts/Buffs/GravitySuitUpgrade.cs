using Player;

namespace Buffs
{
    public class GravitySuitUpgrade : Upgrade
    {
        protected override void SetUpgrade()
        {
            PlayerEntity.Instance.UI.DisplayTooltip(
                "You have collected the gravity suit upgrade. You can now move through liquids at normal speed.");
            PlayerEntity.Instance.unlockedGravitySuit = true;
        }
    }
}