using GameManagement;
using Player;


namespace Buffs
{
    public class HealthUpgrade : Upgrade
    {
        protected override void SetUpgrade()
        {
            PlayerEntity.Instance.UI.DisplayTooltip(
                "You have collected a health upgrade. Your health has been extended.");
            
            PlayerEntity.Instance.UI.healthPipsCollected++;
            for (int i = 0; i < PlayerEntity.Instance.healthUpgradesCollected.Length; i++)
            {
                if (LevelManager.Instance.healthUpgrades[i] == this)
                {
                    PlayerEntity.Instance.healthUpgradesCollected[i] = true;
                    break;
                }
            }
            PlayerEntity.Instance.Health.IncreaseMaxHealth();
            Destroy(gameObject);
        }
    }
}