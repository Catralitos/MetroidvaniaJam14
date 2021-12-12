using GameManagement;
using Player;
using UnityEditor;

namespace Buffs
{
    public class DamageUpgrade : Upgrade
    {
        protected override void SetUpgrade()
        {
            PlayerEntity.Instance.damageUpgradesCollected[ArrayUtility.IndexOf(LevelManager.Instance.damageUpgrades, this)] =
                true;
            PlayerEntity.Instance.Combat.IncreaseMaxDamage();
            Destroy(gameObject);
        }
    }
}
