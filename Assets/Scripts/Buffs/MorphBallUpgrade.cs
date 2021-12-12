using Player;

namespace Buffs
{
    public class MorphBallUpgrade : Upgrade
    {
        protected override void SetUpgrade()
        {
            PlayerEntity.Instance.unlockedMorphBall = true;
        }
    }
}   
