using Player;

namespace Buffs
{
    public class TripleBeamUpgrade : Upgrade
    {
        protected override void SetUpgrade()
        {
            PlayerEntity.Instance.UI.DisplayTooltip("You have collected the triple beam upgrade. Your tentacle now fires three beams.");
            PlayerEntity.Instance.unlockedTripleBeam = true;
        }
    }
}
