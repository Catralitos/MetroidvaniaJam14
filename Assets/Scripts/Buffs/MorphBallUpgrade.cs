using Player;

namespace Buffs
{
    public class MorphBallUpgrade : Upgrade
    {
        protected override void SetUpgrade()
        {
            PlayerEntity.Instance.UI.DisplayTooltip(
                "You have collected the morph ball upgrade. Press down after crouching to curl into a ball.");
            PlayerEntity.Instance.unlockedMorphBall = true;
        }
    }
}