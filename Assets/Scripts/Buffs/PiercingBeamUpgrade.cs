using Player;

namespace Buffs
{
    public class PiercingBeamUpgrade : Upgrade
    {
        protected override void SetUpgrade()
        {
            PlayerEntity.Instance.UI.DisplayTooltip(
                "You have collected the piercing beam upgrade. Your shots will pierce through walls, allowing you to hit enemies through them.");
            PlayerEntity.Instance.unlockedPiercingBeam = true;
        }
    }
}