using GameManagement;
using Player;
using UnityEditor;

namespace Buffs
{
    public class HealthUpgrade : Upgrade
    {
        protected override void SetUpgrade()
        {
            PlayerEntity.Instance.healthUpgradesCollected[ArrayUtility.IndexOf(LevelManager.Instance.healthUpgrades, this)] =
                true;
            PlayerEntity.Instance.Health.IncreaseMaxHealth();
            Destroy(gameObject);
        }
    }
}
